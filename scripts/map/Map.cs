using Godot;
using System;

public partial class Map : Node2D
{
	private Player player;
	public Action<Player> PlayerFoundInvoker;
	public override void _Ready()
	{
		foreach (Node child in GetChildren())
			if (child is Player child1)
				player = child1;
		if (player != null)
			PlayerFoundInvoker?.Invoke(player);
		
	}
}
