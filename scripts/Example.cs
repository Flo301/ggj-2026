using Godot;
using System;



public partial class Example : Interactable
{
    public override void _Ready()
    {
        base._Ready();
    }

    protected override void onInteract()
    {
        GlobalState.Instance.openDialog("demoDialog");
    }
}
