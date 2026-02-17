using Godot;
using System;
using System.Linq;
using SAO.scripts.weapons;

public partial class Player : Unit
{
	[Export] float Amplitude = 10;
	[Export] CpuParticles2D left, right;

	private float time = 0;
	public float Xp = 0;
	public float XpToNextLevel = 50;
	public float XpAddModifier = 1.0f;


	
	public override void _PhysicsProcess(double delta)
	{
		float dt = (float)delta;
		time += dt;
		Position += Vector2.Up * Mathf.Sin(time*4) * dt * Amplitude;
		Movepos(dt);
		taka();
		MoveAndSlide();
	}
	
	#region Movement
	void Movepos(float dt)
	{
		Vector2 azov = Input.GetVector("a", "d", "w", "s");
		azov = azov.Normalized();
		Velocity += azov * dt * Stats.Speed;
		if (azov.X != 0) {
			var hi = azov.X > 0;
			right.Emitting = !hi;
			left.Emitting = hi;
		} else {
			right.Emitting = true;
			left.Emitting = true;
		}
	}

	void taka()
	{
		if (Input.IsActionJustPressed("lm")) {
			var wep = GetChildren().OfType<Weapon>().ToList();
			foreach (var w in wep)
				w.Trigger.RequestAttack((GetGlobalMousePosition()-GlobalPosition).Angle());
		}
	}
	#endregion
}
