using Godot;
using System;

public abstract partial class Interactable : Area3D
{
    [Export]
    public string InteractionText = "Interact";

    [Export]
    public string Dialog = null;

    public override void _Ready()
    {
        base._Ready();
        Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
        Connect("body_exited", new Callable(this, nameof(OnBodyExited)));
    }

    private void OnBodyEntered(Node body)
    {
        if (body is CharacterController character)
        {
            character.SetInteractable(this);
        }
    }

    private void OnBodyExited(Node body)
    {
        if (body is CharacterController character)
        {
            character.RemoveInteractable(this);
        }
    }

    public void Interact()
    {
        if (Dialog != null)
        {
            var dialog = GlobalState.Instance.openDialog(Dialog);
            dialog.Interact += () => onInteract();
        }
        else
        {
            onInteract();
        }
    }

    protected abstract void onInteract();
}
