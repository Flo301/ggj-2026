using Godot;
using System;

public partial class Example : Interactable
{
    public override void OnInteract()
    {
        GetTree().Quit();
    }
}
