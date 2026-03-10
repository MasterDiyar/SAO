using Godot;
using System;
using System.Collections.Generic;

public partial class Eartr : TileMapLayer
{
	[Export] public float Lenght = 7;
	[Export] public Player pl;
	private Timer t;
	public override void _Ready()
	{
		t = GetNode<Timer>("Timer");
		t.Start();
		t.Timeout += TOnTimeout;
		pl ??= GetTree().GetFirstNodeInGroup("player") as Player;
	}

	private List<Vector2I> previousTiles = [];

	private void TOnTimeout()
	{
		if (pl == null) return;
		var playerTile = LocalToMap(pl.GlobalPosition);

		foreach (var pos in previousTiles)
			SetCell(pos, -1);
		
		previousTiles.Clear();

		for (int x = -(int)Lenght; x <= (int)Lenght; x++) {
			for (int y = -(int)Lenght; y <= (int)Lenght; y++) {
				if (!(Mathf.Abs(x) + Mathf.Abs(y) <= Lenght)) continue;
				var targetTile = playerTile + new Vector2I(x, y);
				SetCell(targetTile, 0, Vector2I.Zero);
				previousTiles.Add(targetTile); 
			}
		}
	}
}
