using Godot;
using System;
using System.Linq;

namespace SAO.scripts.weapons;

public partial class Weapon : BaseWeapon
{
	
	[Export] public BulletFactory Factory;
	[Export] public BulletSpawner Spawner;
	[Export] public bool randomA = false;
	public override void _Ready()
	{
		base._Ready();
		Factory ??= GetChildren().OfType<BulletFactory>().FirstOrDefault();
		Spawner ??= GetChildren().OfType<BulletSpawner>().FirstOrDefault();
	}

	protected override void OnShootRequested(float angle)
	{
		float randomAngle = randomA ? GD.Randf() * Mathf.Tau : angle;

		var bullets = Factory.CreateBullets(GlobalPosition, randomAngle);
		var dm = damageType switch{ 
			1 => daddy.Stats.Speed * daddy.Stats.SpeedMultiplier / 30,
			2 => daddy.Stats.Intellegence * daddy.Stats.IntellegenceMultiplier * daddy.Stats.Mp * daddy.Stats.MpMultiplier / 100,
			3 => daddy.Stats.Strength * daddy.Stats.StrengthMultiplier};
		
		Spawner.Spawn(bullets,  GetParent(), dm , (GD.Randf() < daddy.Stats.KritChance) ? daddy.Stats.KritMultiplier : 1);
	}
}