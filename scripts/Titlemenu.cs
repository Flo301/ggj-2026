using Godot;
using System;

public partial class Titlemenu : Control
{
	public void StartGame()
	{
		
	}
	
	public void LoadSave()
	{
		
	}
	
	public void OpenSettings()
	{
		
	}
	
	public void QuitGame()
	{
		PackedScene popupWindow = GD.Load<PackedScene>("res://scenes/prefabs/popup_screen.tscn");
		var popupWindowInstance = popupWindow.Instantiate<PopupScreen>();
		//ToDo: Add Translation text
		popupWindowInstance.Init("[color=red]WEEEE [color=yellow]WOOO [color=red]WEEE [color=yellow]WOOO", "You are currently trying to quit this awesome and super interesting game that you have right in front of you, are you sure that you REALLY REALLY want to leave us all alone??", "Yes", "No...", () => {GetTree().Quit();});
		AddChild(popupWindowInstance);
	}
}
