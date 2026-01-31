


using System.Collections.Generic;
using Godot;

partial class GlobalState : Node
{
    [Signal]
    public delegate void DialogDoneEventHandler(string dialogName);

    public static GlobalState Instance { get; private set; }
    public string LastScene { get; internal set; }

    private List<string> doneDialogs = [];
    public List<string> availableEmotions = ["happy"];

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

    public void openDialog(string dialogName)
    {
        if (isDialogOpen)
        {
            return;
        }

        var dialog = DialogParser.LeadDialog(dialogName);

        // load the dialog scene
        var scene = GD.Load<PackedScene>("scenes/prefabs/dialogue_ui.tscn");
        var instance = scene.Instantiate<DialogueUi>();
        GetTree().Root.FindChild("InGameUi", true, false).AddChild(instance);
        instance.Init(dialog);
    }

    public void AddDialogDone(string dialogName)
    {
        if (!HasDoneDialog(dialogName))
        {
            doneDialogs.Add(dialogName);
            EmitSignal(SignalName.DialogDone, dialogName);
        }
    }
}