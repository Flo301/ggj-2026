using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public partial class DialogueUi : Control
{

	PackedScene MenuItemScene = GD.Load<PackedScene>("scenes/prefabs/container_option.tscn");

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
		jumpToSection(newDialog.StartSectionId);
	}

	public override void _Ready()
	{
		base._Ready();
		clearMenuEntries();
	}

	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		if (@event is InputEventMouseButton mouseButtonEvent && mouseButtonEvent.ButtonIndex == Godot.MouseButton.Left && waitForNext)
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
		Input.MouseMode = Input.MouseModeEnum.Captured;
	}

	public override void _EnterTree()
	{
		base._ExitTree();
		Input.MouseMode = Input.MouseModeEnum.Visible;
	}


	public DialogLineBase getCurrentLine()
	{
		if (activeSection.Lines.Count <= activeLine)
		{
			return null;
		}
		return activeSection.Lines[activeLine].DialogLine;
	}

	public async Task handleCurrentLine()
	{
		var line = getCurrentLine();
		GD.Print(line);

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
		GlobalState.Instance.setDoneDialog(dialog.name + "." + name);
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
				return GlobalState.Instance.hasDoneDialog(dialog.name + "." + condition.Value);
			case "hasEmotion":
				// Check if the player has the given emotion
				break;
		}
		return false;
	}
}
