using Godot;
using System;

public partial class Example : Interactable
{
    public override void _Ready()
    {
        base._Ready();
        var dialog = DialogParser.LeadDialog("demoDialog");
        GD.Print("This dialog has " + dialog.Sections.Count + " Sections.");
    }

    public override void OnInteract()
    {
        GetTree().Quit();
    }
}
