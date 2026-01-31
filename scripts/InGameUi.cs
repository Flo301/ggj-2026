using Godot;
using System;

public partial class InGameUi : Control
{
	static public InGameUi Instance { get; private set; }

	private Label _interactionLabel;
	private Control _interactionContainer;

	public override void _Ready()
	{
		Instance = this;
		_interactionLabel = GetNode<Label>("%InteractLabel");
		_interactionContainer = GetNode<Control>("%ContainerInteract");
	}

	public void ShowInteractionText(string text)
	{
		_interactionLabel.Text = text;
		_interactionContainer.Visible = true;
	}

	public void HideInteractionText()
	{
		_interactionContainer.Visible = false;
	}
}
