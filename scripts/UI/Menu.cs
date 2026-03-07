using Godot;
using System;

public partial class Menu : Control
{
	[Export] private Button play, opt, exit;

	public override void _Ready()
	{
		play.Pressed += PlayOnPressed;
	}

	private void PlayOnPressed()
	{
		
		
		QueueFree();
	}
}
