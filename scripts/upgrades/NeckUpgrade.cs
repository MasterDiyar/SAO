using Godot;
using System;
using SAO.scripts.weapons;

public partial class NeckUpgrade : UpgradeCard
{
	[Export] private int whichOne = 0;

	PackedScene[] weaponScenes = [
		GD.Load<PackedScene>("res://scenes/weapons/sob_chach.tscn"),
		GD.Load<PackedScene>("res://scenes/bullet/")
	];



	protected override void ApplyUpgradeToPlayer()
	{
		var player = GetTree().GetFirstNodeInGroup("player") as Player;
		Weapon weapon = player?.GetNode<Weapon>("SobChach");
		if (weapon is null) {
			GD.PrintErr("a ya hochu skazat: vozmi telefon detka NeckUpgrade.cs");
			return;
		}
		switch (whichOne) {
			case 1:
				Weapon doubler = weaponScenes[0].Instantiate<Weapon>();
				doubler.Factory.Count = 2;
				doubler.Factory.AngleOffset = 0.2618f;
				doubler.Factory.BetweenAngle = 0.5236f;
				player?.AddChild(doubler);
				break;
			case 2:
				weapon.Factory.Count = 5;
				weapon.Factory.BetweenAngle = float.Tau/5f;
				break;
			case 3:
				weapon.Factory.BulletScene = weaponScenes[1];
				break;
		}
	}
}
