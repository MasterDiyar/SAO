using Godot;
using System;
using System.Linq;
using SAO.scripts.bullets;

public partial class Smookey : Bullet
{
	[Export] private Sprite2D[] Spores;
	private float[] angles, scales;
	private float hey = 0;

	private CollisionShape2D sg;

	public override void _Ready()
	{
		base._Ready();
		sg = GetNode<CollisionShape2D>("CollisionShape2D");
		angles = new float[Spores.Length];
		scales = new float[Spores.Length];

		for (int i = 0; i < Spores.Length; i++)
		{
			angles[i] = GD.Randf() * float.Tau;
			scales[i] = GD.Randf()+0.8f;
			Spores[i].Scale *= scales[i];
		}
	}

	public override void Movement(float dt)
	{
		hey += dt;
		float maxScale = 0f;
		for (int i = 0; i < Spores.Length; i++)
		{
			Spores[i].Position += Vector2.FromAngle(angles[i]) * float.Sin(hey + angles[i]) * 32 * dt ;
			scales[i] +=  float.Sin(hey + angles[i]) * dt;
			Spores[i].Scale = Vector2.One * scales[i];
			if (scales[i] > maxScale) 
				maxScale = scales[i]; 
		}
		sg.Scale = Vector2.One * maxScale;
	}
}
