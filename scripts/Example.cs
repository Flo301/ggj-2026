using Godot;
using System;



public partial class Example : Interactable
{
    public override void _Ready()
    {
        base._Ready();
        var dialog = DialogParser.LeadDialog("demoDialog");
        GD.Print("This dialog has " + dialog.Sections.Count + " Sections.");

        // load the dialog scene
        var scene = GD.Load<PackedScene>("scenes/prefabs/dialogue_ui.tscn");
        var instance = scene.Instantiate<DialogueUi>();
        GetTree().Root.FindChild("InGameUi", true, false).AddChild(instance);
        instance.Init(dialog);
    }

    public override void OnInteract()
    {
        GetTree().Quit();
    }
}
