using Godot;
using System;
using SAO.scripts.weapons;

public partial class RedRoom : BaseWeapon
{
	[Export] private Sprite2D sword;
	[Export] Area2D swordArea;
	[Export] private Line2D lien;
	private float speed = 0, SpeedMultiplier = 10;
	public Action OneCycle;
	
	private float flyDuration = 4, nowFlyTime = 0, inverted = 1, flyAngle = 0;
	
	bool isFlying = false;
	public override void _Ready()
	{
		base._Ready();
		flyDuration = 3* Trigger.AttackSpeed/4;
		speed = daddy.Stats.Strength * SpeedMultiplier;
		swordArea.BodyEntered += OnSwordAreaEntered;
	}

	void OnSwordAreaEntered(Node2D body)
	{
		if (body is Unit uit)
		{
			if (uit == GetParent<Unit>()) return;
			uit.TakeDamage(daddy.Stats.Strength * SpeedMultiplier / 20 * (GD.Randf() < daddy.Stats.KritChance ? daddy.Stats.KritMultiplier : 1));
		}
	}

	public override void _Process(double delta)
	{
		var dt = (float)delta;
		if (isFlying) {
			if (nowFlyTime > flyDuration) {
				nowFlyTime = 0;
				if (inverted == -3) 
					isFlying = false;
				else {
					inverted = -3;
					OneCycle?.Invoke();
				}
			}
			nowFlyTime += dt * float.Abs(inverted);
			sword.Position += Vector2.FromAngle(flyAngle) * inverted * dt * speed;
			
			lien.SetPointPosition(1, sword.Position);
		}
		else
		{
			sword.Rotation = (GetGlobalMousePosition() - GetGlobalPosition()).Angle();
			sword.Position = Vector2.Zero;
			lien.ClearPoints();
		}
	}
	
	protected override void OnShootRequested(float angle)
	{
		if (isFlying) return;
		isFlying = true;
		inverted = 1;
		speed = daddy.Stats.Strength * SpeedMultiplier;
		flyAngle = angle;
		sword.Rotation = angle;
		
		lien.SetPoints([v2(0, 0), v2(0, 0)]);
	}
	
	Vector2 v2(float x1,float x2) => new(x1,x2);
}
