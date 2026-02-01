


using System.Collections.Generic;
using Godot;

partial class GlobalState : Node
{
    public static GlobalState Instance { get; private set; }
    public string LastScene { get; internal set; }

    public List<string> doneDialogs = [];
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

    public void resetDay()
    {
        doneDialogs = [];
        foreach (var emotion in newEmotions)
        {
            availableEmotions.Add(emotion);
        }
    }
}