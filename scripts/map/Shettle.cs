using Godot;
using System;

public partial class Shettle : StaticBody2D
{
	[Export] private AnimationPlayer anum;
	[Export] private PointLight2D blinkLight, energyfallLight;
	private Area2D seba;
	public override void _Ready()
	{
		seba = GetNode<Area2D>("Shop");
		anum.Play("infiniBlink");

		seba.BodyEntered += OnPLayerEntered;
		seba.BodyExited  += OnPlayerExited;
	}

	void OnPLayerEntered(Node body)
	{
		if (body is not Player pl) return;
		
		PlayerEntered = true;
	}

	void OnPlayerExited(Node body)
	{
		if (body is not Player pl) return;
		
		PlayerEntered = false;
	}
	
	bool PlayerEntered = false;

	

	public override void _Process(double delta)
	{
	}
}
