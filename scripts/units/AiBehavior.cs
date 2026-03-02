using Godot;
using System;

public partial class AiBehavior : Node2D
{
	[Export] public int Escalation = 0;
	private Player player;
	
	Timer timer;
	public override void _Ready()
	{
		timer = new Timer() { WaitTime = 3 };
		timer.Start();
		player = GetTree().GetFirstNodeInGroup("player") as Player;
		
	}

	public override void _Process(double delta)
	{
		
	}
}
