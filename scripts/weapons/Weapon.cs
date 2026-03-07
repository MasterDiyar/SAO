using Godot;
using System;
using System.Linq;

namespace SAO.scripts.weapons;

public partial class Weapon : Node2D
{
	[Export] public  WeaponTrigger Trigger;
	[Export] public BulletFactory Factory;
	[Export] public BulletSpawner Spawner;
	[Export] public bool randomA = false;
	public override void _Ready()
	{
		Trigger.ShootRequested += OnShootRequested;
		Trigger ??= GetChildren().OfType<WeaponTrigger>().FirstOrDefault();
		Factory ??= GetChildren().OfType<BulletFactory>().FirstOrDefault();
		Spawner ??= GetChildren().OfType<BulletSpawner>().FirstOrDefault();
	}

	private void OnShootRequested(float angle)
	{
		float randomAngle = randomA ? GD.Randf() * Mathf.Tau : angle;//(GlobalPosition-GetGlobalMousePosition()).Angle();

		var bullets = Factory.CreateBullets(GlobalPosition, randomAngle);

		Spawner.Spawn(bullets,  GetParent());
	}
}