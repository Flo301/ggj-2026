using Godot;
using System;

public partial class SettingsScreen : Control
{
	
	public void Init()
	{
		//ToDo: add load settings
	}
	
	public void Close()
	{
		this.QueueFree();
	}
	
	public void Apply()
	{
		//ToDo: add save settings
		Close();
	}
	
	public void GetAllSettings()
	{
		var Settings = GetNode<SettingRow>("%ContainerSettings").GetChildren();
		
	}
}
