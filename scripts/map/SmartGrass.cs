using Godot;
using System;

public partial class SmartGrass : MultiMeshInstance2D
{
	[Export] public Node2D Player; // Перетащите игрока сюда в инспекторе

	private ShaderMaterial _material;
	
	[Export] public Vector2 AreaSize = new Vector2(800, 400);
	[Export] public float MinScale = 0.8f;
	[Export] public float MaxScale = 1.2f;

	public override void _Ready()
	{
		
		if (Multimesh == null) return;

		var rng = new RandomNumberGenerator();
		rng.Randomize();

		int count = Multimesh.InstanceCount;

		for (int i = 0; i < count; i++)
		{
			float x = rng.RandfRange(0, AreaSize.X);
			float y = rng.RandfRange(0, AreaSize.Y);
			float scale = rng.RandfRange(MinScale, MaxScale);

			Transform2D transform = Transform2D.Identity;
			transform = transform.Scaled(Vector2.One * scale);
			transform = transform.Translated(new Vector2(x, y));
			

			Multimesh.SetInstanceTransform2D(i, transform);

			//Multimesh.SetInstanceCustomData(i, new Color(rng.Randf(), 0, 0, 0));
		}
		
		_material = Material as ShaderMaterial;
	}
	
	public override void _Process(double delta)
	{
		if (Player != null && _material != null)
		{
			_material.SetShaderParameter("player_pos", Player.GlobalPosition);
		}
	}
}