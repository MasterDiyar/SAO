using Godot;
using System;
using Godot.Collections;

public partial class SmartGrass : MultiMeshInstance2D
{
	[Export] public Node2D Player; 

	private ShaderMaterial _material;
	
	public Array<Rect2> NoSpawnRects;
	
	[Export] public Vector2 AreaSize = new Vector2(800, 400);
	[Export] public float MinScale = 0.8f;
	[Export] public float MaxScale = 1.2f;

	public override void _Ready()
	{
		_material = Material as ShaderMaterial;
	}

	public void Generate()
	{
		if (Multimesh == null) return;

		var rng = new RandomNumberGenerator();
		rng.Randomize();

		int count = Multimesh.InstanceCount;

		for (int i = 0; i < count; i++)
		{
			Vector2 pos = Vector2.Zero;
			bool isValid = false;
			int attempts = 0;

			while (!isValid && attempts < 10) 
			{
				pos = new Vector2(rng.RandfRange(0, AreaSize.X), rng.RandfRange(0, AreaSize.Y));
				isValid = true;
				attempts++;

				if (NoSpawnRects != null)
				{
					foreach (Rect2 rect in NoSpawnRects)
					{
						if (rect.HasPoint(pos))
						{
							isValid = false;
							break;
						}
					}
				}
			}
			
			float scale = rng.RandfRange(MinScale, MaxScale);

			Transform2D transform = Transform2D.Identity;
			transform = transform.Scaled(Vector2.One * scale);
			transform = transform.Translated(pos);
			

			Multimesh.SetInstanceTransform2D(i, transform);
		}
	}
	
	public override void _Process(double delta)
	{
		if (Player == null || _material == null) return;
		if (IsInstanceValid(Player))
			_material.SetShaderParameter("player_pos", Player.GlobalPosition);
		else SetProcess(false);
	}
}