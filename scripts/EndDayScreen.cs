using Godot;
using System;
using System.Collections.Generic;

public partial class EndDayScreen : Control
{
	[Export]
	public BoxContainer ContentContainer;
	[Export]
	public RichTextLabel NoNewMaskLabel;
	public Action Callback;

	/*
	public List<string> test= ["afraid", "angry", "happy", "loving", "sad", "surprised", "thankful"];
	public override void _Ready()
	{
		Init(test, () => EndDay());
	}
	*/

	public void Init(List<string> newEmotions, Action callback)
	{
		Callback = callback;

		var scene = GD.Load<PackedScene>("res://scenes/prefabs/container_mask.tscn");
		if (newEmotions.Count <= 0)
		{
			NoNewMaskLabel.Visible = true;
			return;
		}
		foreach (string emotion in newEmotions)
		{
			var instance = scene.Instantiate<ContainerMask>();
			ContentContainer.AddChild(instance);
			instance.Init(emotion, $"GGJ2026_MENU_NEUE_MASKE_{emotion.ToUpper()}");
		}
	}

	public void EndDay()
	{
		Callback();
		QueueFree();
	}
}
