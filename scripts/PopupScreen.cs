using Godot;
using System;

public partial class PopupScreen : Control
{
	public Action ConfirmAction;
	
	public void Init(string title, string content, string confirm, string deny, Action actionAccept)
	{
		GetNode<RichTextLabel>("%PopupTitle").Text = title;
		GetNode<RichTextLabel>("%PopupContent").Text = content;
		GetNode<Button>("%ButtonConfirm").Text = confirm;
		GetNode<Button>("%ButtonDeny").Text = deny;
		ConfirmAction = actionAccept;
	}
	
	public void AcceptAction()
	{
		ConfirmAction.Invoke();
		this.QueueFree();
	}
	
	public void DenyAction()
	{
		this.QueueFree();
	}
	
}
