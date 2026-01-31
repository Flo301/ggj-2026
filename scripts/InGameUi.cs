using Godot;
using System;

public partial class InGameUi : Control
{
    static public InGameUi Instance { get; private set; }

    private Label _interactionLabel;

    public override void _Ready()
    {
        Instance = this;
        _interactionLabel = GetNode<Label>("InteractLabel");
    }

    public void ShowInteractionText(string text)
    {
        _interactionLabel.Text = text;
        _interactionLabel.Visible = true;
    }

    public void HideInteractionText()
    {
        _interactionLabel.Visible = false;
    }
}
