using Godot;
using System;

public partial class Player : Unit
{
	[Export] float Amplitude = 10;
	[Export] CpuParticles2D left, right;
	[Export] float speed = 20;
	public override void _Ready()
	{
	}

	private float time = 0;
	public override void _Process(double delta)
	{
		float dt = (float)delta;
		time += dt;
		Position += Vector2.Up * Mathf.Sin(time) * dt * Amplitude;
		Movepos(dt);
	}

	void Movepos( float dt)
	{
		Vector2 azov = Input.GetVector("a", "d", "w", "s");
		azov = azov.Normalized();
		Position += azov * dt * speed;
		if (azov.X != 0)
		{
			var hi = azov.X > 0;
			right.Emitting = !hi;
			left.Emitting = hi;
		}
		else
		{
			right.Emitting = true;
			left.Emitting  = true;
		}
	}
	
	
}
