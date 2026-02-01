using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

public partial class DialogueUi : Control
{


	[Signal]
	public delegate void InteractEventHandler();

	PackedScene MenuItemScene = GD.Load<PackedScene>("scenes/prefabs/container_option.tscn");
	private ColorRect background => GetNode<ColorRect>("Background");

	Dialog dialog;
	Section activeSection;
	int activeLine = 0;
	int currentVisibleCharacters = 0;

	bool waitForNext = false;

	[Export]
	public RichTextLabel DialogueTitle;
	[Export]
	public RichTextLabel DialogueContent;
	[Export]
	public TextureRect CharacterTexture;
	[Export]
	public BoxContainer OptionsContainer;

	public void Init(Dialog newDialog)
	{
		dialog = newDialog;
		CallDeferred(MethodName.jumpToSection, newDialog.StartSectionId);
	}

	public override void _Ready()
	{
		base._Ready();
	}

	public void setCharacterImage(string name)
	{
		GD.Print("setCharacterImage called with: " + name);
		if (name == null)
		{
			CharacterTexture.Visible = false;
			return;
		}

		var texture = GD.Load<Texture2D>("res://assets/sprites/dialogue/" + name + ".png");
		if (texture == null)
		{
			CharacterTexture.Visible = false;
			return;
		}
		CharacterTexture.Texture = texture;
		CharacterTexture.Visible = true;
	}

	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		if (
			waitForNext && (
				@event is InputEventMouseButton mouseButtonEvent && mouseButtonEvent.ButtonIndex == MouseButton.Left && mouseButtonEvent.Pressed ||
				@event is InputEventKey keyEvent && (
					keyEvent.Keycode == Key.Enter || keyEvent.Keycode == Key.Space
				) && !keyEvent.Echo && keyEvent.Pressed
			)
		)
		{
			activeLine++;
			waitForNext = false;
			_ = handleCurrentLine();
		}
	}


	void clearMenuEntries()
	{
		var children = OptionsContainer.GetChildren();
		foreach (var child in children)
		{
			child.QueueFree();
		}
	}


	public override void _ExitTree()
	{
		base._ExitTree();
		GlobalState.Instance.isDialogOpen = false;
	}

	public override void _EnterTree()
	{
		base._ExitTree();
		GlobalState.Instance.isDialogOpen = true;
		clearMenuEntries();
		setCharacterImage(null);
	}


	public DialogLineBase getCurrentLine()
	{
		if (activeSection == null || activeSection.Lines.Count <= activeLine)
		{
			return null;
		}
		return activeSection.Lines[activeLine].DialogLine;
	}

	public async Task handleCurrentLine()
	{
		var line = getCurrentLine();

		// If we dont have any lines left we return and end the dialog!
		if (line == null)
		{
			QueueFree();
			return;
		}

		// Handle different line types

		// Jump to next section
		if (line is NextSectionLine nextSectionLine)
		{
			jumpToSection(nextSectionLine.NextSection);
			return;
		}

		// Show background
		if (line is ShowBackgroundLine showBackgroundLine)
		{
			setCharacterImage(showBackgroundLine.Background);
		}

		// Trigger event
		if (line is EventLine eventLine)
		{
			var continueExecution = handledEventArgs(eventLine.Name, eventLine.Value);
			if (!continueExecution)
			{
				return;
			}
		}

		// handle switch line
		if (line is SwitchLine switchLine)
		{
			if (isConditionMet(switchLine.Condition))
			{
				jumpToSection(switchLine.NextSection);
				return;
			}
		}

		// Show options
		if (line is OptionsLine optionsLine)
		{
			foreach (var option in optionsLine.Options)
			{
				// check condition
				if (!isConditionMet(option.Condition))
				{
					continue;
				}

				// add option to menu
				var instance = MenuItemScene.Instantiate<ContainerOption>();
				OptionsContainer.AddChild(instance);
				instance.Init(option);

				instance.Selected += () =>
				{
					clearMenuEntries();
					jumpToSection(option.NextSection);
				};
			}

			return;
		}

		// Show dialog line
		if (line is DialogLine dialogLine)
		{
			DialogueTitle.Text = dialogLine.Speaker;
			DialogueContent.Text = dialogLine.Text;
			DialogueContent.VisibleCharacters = 0;
			_ = updateCurrentVisibleCharacters();
			return;
		}

		activeLine++;
		_ = handleCurrentLine();
	}

	private bool handledEventArgs(string name, string value)
	{
		switch (name)
		{
			case "getEmotion":
				if (!GlobalState.Instance.availableEmotions.Contains(value))
				{
					GlobalState.Instance.availableEmotions.Add(value);
				}
				break;
			case "background":
				switch (value)
				{
					case "black":
						background.Color = Color.FromHtml("black");
						break;
					default:
						background.Color = Color.FromHtml("#00000037");
						break;
				}
				break;
			case "interact":
				GD.Print("Interact");
				EmitSignal(SignalName.Interact);
				break;
			// return false;
			default:
				GD.PushError("Unknown event name: " + name);
				break;
		}

		return true;
	}

	public async Task updateCurrentVisibleCharacters()
	{
		DialogueContent.VisibleCharacters++;
		if (DialogueContent.VisibleCharacters >= DialogueContent.GetTotalCharacterCount())
		{
			waitForNext = true;
			return;
		}

		await ToSignal(GetTree().CreateTimer(0.01), "timeout");
		_ = updateCurrentVisibleCharacters();
	}

	void jumpToSection(string name)
	{
		var dialogkey = dialog.name + "." + name;
		if (!GlobalState.Instance.HasDoneDialog(dialogkey))
		{
			GlobalState.Instance.AddDialogDone(dialog.name + "." + name);
		}
		activeSection = dialog.getSectionById(name);
		activeLine = 0;
		_ = handleCurrentLine();
	}

	public bool isConditionMet(Condition condition)
	{
		// No condition means always true
		if (condition == null)
		{
			return true;
		}


		switch (condition.Type)
		{
			case "didDialog":
				// Check if the player did the given dialog
				return GlobalState.Instance.HasDoneDialog(condition.Value);
			case "hasEmotion":
				// Check if the player has the given emotion
				return GlobalState.Instance.availableEmotions.Contains(condition.Value);
			case "didDays":
				// 
				return GlobalState.Instance.dayCount >= int.Parse(condition.Value);
			default:
				break;
		}
		return false;
	}
}
