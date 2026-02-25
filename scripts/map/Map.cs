using Godot;
using System;

public partial class Map : Node2D
{
	private Player player;
	public Action<Player> PlayerFoundInvoker;
	public override void _Ready()
	{
		player = GetNodeOrNull<Player>("Player");
		if (player != null)
			PlayerFoundInvoker?.Invoke(player);
		
	}
}
