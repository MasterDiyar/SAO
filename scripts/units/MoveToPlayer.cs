using Godot;
using System;

public partial class MoveToPlayer : Node2D
{
	[ExportGroup("Movement")] 
	[Export] private Unit me;
	[Export] float MinDistance = 10; 
	[ExportSubgroup("Jump")]
	[Export] private bool isJumping;
	[ExportSubgroup("Jump")]
	[Export] private float JumpAmplitude = 0;
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}
}
