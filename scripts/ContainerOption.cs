using Godot;
using System;

public partial class ContainerOption : MarginContainer
{
	[Signal]
	public delegate void SelectedEventHandler();
	
	public void WhenSelected()
	{
		EmitSignal(SignalName.Selected);
	}
}
