using Godot;
using System;

public partial class CellsUpgrade : UpgradeCard
{
	private PackedScene[] Weapons = [
		GD.Load<PackedScene>("res://scenes/weapons/o_clad.tscn")
	];
	protected override void ApplyUpgradeToPlayer()
	{
		if (GetTree().GetFirstNodeInGroup("Player") is Unit player)
		{
			int index = level - 1;
			
			if (index >= 0 && index < UpgradeResources.Length) {
				UnitResource upgrade = UpgradeResources[index];
				player.AddChild(Weapons[index].Instantiate());
				
				
			}else 
				GD.PrintErr("LevelLimitException");
		}
	}
}
