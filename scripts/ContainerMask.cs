using Godot;
using System;

public partial class ContainerMask : MarginContainer
{
	[Export]
	public TextureRect Icon;
	[Export]
	public RichTextLabel Text;
	
	public void Init(string emotion, string text)
	{
		Icon.Texture = GetMaskTexture(emotion);
		Text.Text = text;
	}
	
	public Texture2D GetMaskTexture(string emotion)
	{
		return GD.Load<Texture2D>($"res://assets/sprites/masks/mask {emotion}.png");
	}
}
