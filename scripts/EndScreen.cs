using Godot;
using System;

public partial class EndScreen : Control
{
	[Export]
	public Control GoodEnding;
	[Export]
	public Control BadEnding;
	
	public void Init(bool IsGoodEnding)
	{
		GoodEnding.Visible = IsGoodEnding;
		BadEnding.Visible = !IsGoodEnding;
	}
	
	public void TryAgain()
	{
		GetTree().ChangeSceneToFile("res://scenes/levels/Apartment.tscn");
	}
	
	public void ToTitle()
	{
		GetTree().ChangeSceneToFile("res://scenes/titlemenu.tscn");
	}
}
