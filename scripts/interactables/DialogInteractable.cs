using Godot;
using System;


public partial class DialogInteractable : Interactable
{
    public override void _Ready()
    {
        base._Ready();
    }

    protected override void onInteract()
    {
        // nothing dialogs already handled in base class
    }
}
