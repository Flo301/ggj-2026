


using System.Collections.Generic;
using Godot;

partial class GlobalState : Node
{
	[Signal]
	public delegate void DialogDoneEventHandler(string dialogName);
	[Signal]
	public delegate void DialogEndEventHandler(string dialogName);

	public static GlobalState Instance { get; private set; }
	public string LastScene { get; internal set; }

	private List<string> doneDialogs = [];
	public List<string> availableEmotions = ["happy"];
	public List<string> newEmotions = [];
	public int dayCount = 0;

	public bool isDialogOpen = false;

	public override void _Ready()
	{
		base._Ready();
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			GD.PushError("GlobalState already instanciated");
		}
	}

	public bool HasDoneDialog(string dialogName)
	{
		return doneDialogs.Contains(dialogName);
	}

	public DialogueUi openDialog(string dialogName)
	{
		if (isDialogOpen)
		{
			return null;
		}

		var dialog = DialogParser.LeadDialog(dialogName);

		// load the dialog scene
		var scene = GD.Load<PackedScene>("scenes/prefabs/dialogue_ui.tscn");
		var instance = scene.Instantiate<DialogueUi>();
		GetTree().Root.FindChild("InGameUi", true, false).AddChild(instance);
		instance.Init(dialog);
		return instance;
	}

	public void endDay()
	{
		// load EndDayScreen
		var scene = GD.Load<PackedScene>("res://scenes/prefabs/end_day_screen.tscn");
		var instance = scene.Instantiate<EndDayScreen>();
		GetTree().GetCurrentScene().AddChild(instance);
		instance.Init(newEmotions, () => resetDay());
	}

	public void resetDay()
	{
		// delete and respawn scene
		doneDialogs = [];
		foreach (var emotion in newEmotions)
		{
			availableEmotions.Add(emotion);
		}
		GetTree().ChangeSceneToFile("res://scenes/levels/Apartment.tscn");
	}

	public void AddDialogDone(string dialogName)
	{
		if (!HasDoneDialog(dialogName))
		{
			doneDialogs.Add(dialogName);
			EmitSignal(SignalName.DialogDone, dialogName);
		}
	}

	public void OnDialogEnd(string dialogName)
	{
		EmitSignal(SignalName.DialogEnd, dialogName);
	}
}
