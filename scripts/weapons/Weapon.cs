using Godot;
using System;

namespace SAO.scripts.weapons;

public partial class Weapon : Node2D
{
	[Export] public  WeaponTrigger Trigger;
	[Export] private BulletFactory _factory;
	[Export] private BulletSpawner _spawner;
	[Export] private bool randomA = false;
	public override void _Ready()
	{
		Trigger.ShootRequested += OnShootRequested;
	}

	private void OnShootRequested(float angle)
	{
		float randomAngle = randomA ? GD.Randf() * Mathf.Tau : angle;//(GlobalPosition-GetGlobalMousePosition()).Angle();

		var bullets = _factory.CreateBullets(GlobalPosition, randomAngle);

		_spawner.Spawn(bullets,  GetParent());
	}
}