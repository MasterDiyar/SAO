using Godot;
using System;

public partial class Grass : Node2D
{
	private Map map;
	private Player player;
	void AssignPlayer()
	{
		player ??= GetTree().GetFirstNodeInGroup("player") as Player;
		foreach (var child in GetChildren())
			if (child is SmartGrass ass)
				ass.Player = player;
	}
	public override void _Ready()
	{
		map = GetParent<Map>();
		map.PlayerFoundInvoker += (_player) => { player = _player; AssignPlayer(); };
	}

}
