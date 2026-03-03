using Godot;
using System;

public partial class Rost : Timer
{
	[Export] public PackedScene toWhom;
	public override void _Ready()
	{
		Start();
		Timeout += OnTimeout;
	}

	private void OnTimeout()
	{
		Stop();
		var a = toWhom.Instantiate<Node2D>();
		a.Position = GetParent<Node2D>().GlobalPosition;
		GetTree().Root.AddChild(a);
		GetParent().QueueFree();
	}
}
