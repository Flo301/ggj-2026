using Godot;
using System;

public partial class PauseScreen : Control
{
	public void Close()
	{
		this.QueueFree();
	}
	
	public void ToMainMenu()
	{
		GetTree().ChangeSceneToFile("res://scenes/titlemenu.tscn");
	}
}
