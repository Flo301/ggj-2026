using Godot;
using System;

public partial class DialogueUi : Control
{
	[Export]
	public RichTextLabel DialogueTitle;
	[Export]
	public RichTextLabel DialogueContent;
	[Export]
	public TextureRect CharacterTexture;
	
	public void Init()
	{
		
	}
}
