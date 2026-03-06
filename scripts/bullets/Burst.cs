using Godot;
using System;

public partial class Burst : Node2D
{
	[Export] private Sprite2D[] winds;
	[Export] private float time = 5;
	[Export] private float randAdd = 0.04f;
	[Export] private float Speed = 20f;
	float currentTime = 0;
	private Vector2[] initialPos, initialScale;
	private float speed;
	private bool[] lls;
	private float atol = 0.1f;

	public override void _Ready()
	{
		speed = Speed;
		lls = new bool[winds.Length];
		initialPos = new Vector2[winds.Length];
		initialScale = new Vector2[winds.Length];
		for (var i = 0; i < winds.Length; i++)
		{
			lls[i] = true;
			var wind = winds[i];
			initialPos[i] = wind.Position;
			initialScale[i] = wind.Scale;
		}
	}

	public override void _Process(double delta)
	{
		float dt = (float)delta;
		currentTime += dt;
		GD.Print($"Current Time: {currentTime}");
		if (currentTime >= time) {
			GD.Print("Burst");
			bool allDone = false;
			for (var i=0; i < winds.Length; i++) {
				if (GD.Randf() > 0.5f)
					lls[i] = false;
				allDone |= lls[i];
			} 
			if (!allDone) {
				foreach (var wind in winds) wind.Visible = false;
				SetProcess(false);
			}
			time += randAdd;
		}else {
			if (currentTime >= atol) {
				atol += 0.3f;
				foreach (var wind in winds)
					wind.Visible = !wind.Visible;
			}
		}
		

		foreach (var wind in winds)
		{
			wind.Position += Vector2.FromAngle(wind.Rotation+float.Pi/2f) * speed * (wind.Scale.X + wind.Scale.Y)/2f * dt;
			wind.Scale += wind.Scale * 0.025f * dt;
			wind.Rotation += dt * ((wind.Position.X > 0) ? -1 : 1) * 7f * 1/wind.Position.Length();
		}

		speed -= speed/2f*dt;
	}

	internal void On()
	{
		SetProcess(true);
		for (var i = 0; i < winds.Length; i++)
		{
			var wind = winds[i];
			wind.Visible = true;
			wind.Position = initialPos[i];
			wind.Scale = initialScale[i];
			
		}
		speed = Speed;
	}
}
