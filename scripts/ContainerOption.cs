using Godot;
using System;

public partial class ContainerOption : MarginContainer
{
	Action<OptionsLine> callback;

	[Signal]
	public delegate void SelectedEventHandler();
	[Signal]
	public delegate void HoveredEventHandler();

	public void WhenSelected()
	{
		EmitSignal(SignalName.Selected);
	}

	public void Init(DialogOption option)
	{
		GetNode<Button>("HBoxContainerOption/ButtonOption").Text = option.Text;
	}
}
