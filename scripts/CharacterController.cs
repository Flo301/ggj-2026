using Godot;
using System;

public partial class CharacterController : CharacterBody3D
{
    [Export] public float Speed = 5.0f;

    private float _gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
    private Interactable _currentInteractable;

    public override void _Ready()
    {
        Input.MouseMode = Input.MouseModeEnum.Captured;
    }

    public void SetInteractable(Interactable interactable)
    {
        _currentInteractable = interactable;
        GD.Print("Interactable in range: " + interactable.InteractionText);
    }

    public void RemoveInteractable(Interactable interactable)
    {
        if (_currentInteractable == interactable)
            _currentInteractable = null;

        GD.Print("Interactable out of range: " + interactable.InteractionText);
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector3 velocity = Velocity;

        // Apply gravity
        if (!IsOnFloor())
        {
            velocity.Y -= _gravity * (float)delta;
        }

        // Get input direction relative to character's forward direction
        Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        Vector3 forward = -Transform.Basis.Z;
        Vector3 right = -Transform.Basis.X;
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
            if (keyEvent.Keycode == Key.Escape)
            {
                Input.MouseMode = Input.MouseMode == Input.MouseModeEnum.Captured
                    ? Input.MouseModeEnum.Visible
                    : Input.MouseModeEnum.Captured;
            }
            else if (keyEvent.Keycode == Key.E && _currentInteractable != null)
            {
                _currentInteractable.OnInteract();
            }
        }
    }
}
