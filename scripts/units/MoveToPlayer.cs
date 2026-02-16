using Godot;
using System;

public partial class MoveToPlayer : Node2D
{
	[ExportGroup("Movement")] 
	[Export] private Unit me, player;
	[Export] float MinDistance = 10; 
	[ExportSubgroup("Jump")]
	[Export] private bool isJumping;
	[ExportSubgroup("Jump")]
	[Export] private float JumpAmplitude = 0;
	public override void _Ready()
	{
		player = (Unit)GetTree().GetFirstNodeInGroup("player");
	}

	public override void _Process(double delta)
	{
		
		
		
	}
}
