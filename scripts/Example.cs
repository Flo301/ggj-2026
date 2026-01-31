using Godot;
using System;



public partial class Example : Interactable
{
    public override void _Ready()
    {
        base._Ready();
    }

    public override void OnInteract()
    {
        GlobalState.Instance.openDialog("demoDialog");
    }
}
