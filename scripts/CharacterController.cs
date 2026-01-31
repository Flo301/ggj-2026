using Godot;
using System;

public partial class CharacterController : CharacterBody3D
{
    [Export] public float Speed = 5.0f;

    private float _gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
    private Interactable _currentInteractable;

    public override void _Ready()
    {
        Input.MouseMode = Input.MouseModeEnum.Visible;
    }

    public void SetInteractable(Interactable interactable)
    {
        _currentInteractable = interactable;
        InGameUi.Instance.ShowInteractionText(interactable.InteractionText);
    }

    public void RemoveInteractable(Interactable interactable)
    {
        if (_currentInteractable == interactable)
        {
            _currentInteractable = null;
            InGameUi.Instance.HideInteractionText();
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        // If a dialog is open, prevent character movement
        if (GlobalState.Instance.isDialogOpen)
        {
            Velocity = Vector3.Zero;
            MoveAndSlide();
            return;
        }

        Vector3 velocity = Velocity;

        // Apply gravity
        if (!IsOnFloor())
        {
            velocity.Y -= _gravity * (float)delta;
        }

        // Get input direction relative to character's forward direction
        Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        Vector3 forward = Vector3.Back;
        Vector3 right = Vector3.Right;
        Vector3 direction = (right * inputDir.X + forward * inputDir.Y).Normalized();

        // Apply movement
        float currentSpeed = Speed;

        if (direction != Vector3.Zero)
        {
            velocity.X = direction.X * currentSpeed;
            velocity.Z = direction.Z * currentSpeed;
        }
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, currentSpeed);
            velocity.Z = Mathf.MoveToward(Velocity.Z, 0, currentSpeed);
        }

        Velocity = velocity;
        MoveAndSlide();
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventKey keyEvent && keyEvent.Pressed)
        {
            if (keyEvent.Keycode == Key.E && _currentInteractable != null)
            {
                _currentInteractable.OnInteract();
            }
        }
    }
}
