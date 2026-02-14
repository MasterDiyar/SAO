using Godot;
using System;
using SAO.scripts.bullets;

public partial class Weapon : Node2D
{
	[Export] PackedScene bulletScene;
	[Export] private float BetweenAngle = 0;
	[Export] private float Scale = 1;
	[Export] private float Offset = 0;
	[Export] int Count = 1;
	[Export] float AttackSpeed = 1;
	[Export] private bool AutoAttack = false;

	private float co = 0;
	public override void _Process(double delta)
	{
		co += (float)delta;
		if (!AutoAttack) return;
		if (!(co > AttackSpeed)) return;
		co = 0;
		Attack(GD.Randf() * Mathf.Tau);
	}

	public void Attack(float angle)
	{
		for (int i = 0; i < Count; i++)
		{
			Bullet bt = bulletScene.Instantiate<Bullet>();
			bt.Rotation = angle + BetweenAngle* i;
			bt.GlobalPosition = GlobalPosition + Vector2.FromAngle(angle) * Offset;
			bt.GlobalScale *= Scale;
			GetTree().Root.AddChild(bt);
		}
		
	}
}
