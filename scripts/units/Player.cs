using Godot;
using System;
using System.Linq;
using SAO.scripts.weapons;

public partial class Player : Unit
{
	[Export] float Amplitude = 10;
	[Export] public float Friction = 2.0f;
	[Export] CpuParticles2D left, right;

	private float time = 0;
	public float Xp = 0;
	public float XpToNextLevel = 50;
	public float XpAddModifier = 1.0f;

	public override void _Ready()
	{
		XpAddModifier = Stats.XpGainMultiplier;
		left.ScaleAmountMin = Scale.X / 2;
		right.ScaleAmountMin = Scale.X / 2;
		left.ScaleAmountMax = Scale.X;
		right.ScaleAmountMax = Scale.X;
		
	}


	public override void _PhysicsProcess(double delta)
	{
		float dt = (float)delta;
		time += dt;
		Position += Vector2.Up * Mathf.Sin(time*4) * dt * Amplitude;
		Movepos(dt);
		taka();
		MoveAndSlide();
	}

	#region Upgrades

	public void AddXp(float xp)
	{
		Xp += Mathf.Clamp(xp * XpAddModifier, 0.1f, xp * XpAddModifier);
		GD.Print("Xp: " + Xp);
		if (!(Xp >= XpToNextLevel)) return;
		while (Xp >= XpToNextLevel)
		{
			Xp -= XpToNextLevel;
			GiveUpgradeCards();
			XpToNextLevel *= 1.6f;
		}
	}

	void GiveUpgradeCards()
	{
		
	}
	

	#endregion
	
	#region Movement
	void Movepos(float dt)
	{
		Vector2 inputDir = Input.GetVector("a", "d", "w", "s").Normalized();
    
		Vector2 targetVelocity = inputDir * Stats.Speed;
    
		Velocity = inputDir != Vector2.Zero ? Velocity.Lerp(targetVelocity, dt * (Stats.Speed / 100.0f)) : Velocity.Lerp(Vector2.Zero, dt * Friction);
		if (inputDir.X != 0) {
			var hi = inputDir.X > 0;
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
