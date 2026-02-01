using Godot;
using System;

public partial class Titlemenu : Control
{
	[Export]
	PackedScene StartScene;
	
	public void StartGame()
	{
		GetTree().ChangeSceneToPacked(StartScene);
	}
	
	public void LoadSave()
	{
		StartGame();
	}
	
	public void OpenSettings()
	{
		PackedScene settingsWindow = GD.Load<PackedScene>("res://scenes/prefabs/settings_screen.tscn");
		var settingsWindowInstance = settingsWindow.Instantiate<SettingsScreen>();
		//ToDo: Add Settings
		settingsWindowInstance.Init();
		AddChild(settingsWindowInstance);
	}
	
	public void QuitGame()
	{
		PackedScene popupWindow = GD.Load<PackedScene>("res://scenes/prefabs/popup_screen.tscn");
		var popupWindowInstance = popupWindow.Instantiate<PopupScreen>();
		//ToDo: Add Translation text
		popupWindowInstance.Init("GGJ2026_MENU_VERLASSEN_TITEL", "GGJ2026_MENU_VERLASSEN_INHALT", "GGJ2026_MENU_JA", "GGJ2026_MENU_NEIN", () => {GetTree().Quit();});
		AddChild(popupWindowInstance);
	}
}
