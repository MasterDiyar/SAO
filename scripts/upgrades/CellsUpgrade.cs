using Godot;
using System;

public partial class CellsUpgrade : UpgradeCard
{
	private PackedScene[] Weapons = [
		GD.Load<PackedScene>("res://scenes/weapons/o_clad.tscn"),
		GD.Load<PackedScene>("res://scenes/weapons/al_omaran.tscn"),
		GD.Load<PackedScene>("res://scenes/weapons/marlow.tscn")
	];
	protected override void ApplyUpgradeToPlayer()
	{
		if (GetTree().GetFirstNodeInGroup("player") is Unit player)
		{
			int index = level - 1;
			
			if (index >= 0 && index < Weapons.Length) {
				var upgrade = Weapons[index];
				player.AddChild(Weapons[index].Instantiate());
			}else
			{
				GD.PrintErr("LevelLimitException", index);
			}
		}
	}
}
