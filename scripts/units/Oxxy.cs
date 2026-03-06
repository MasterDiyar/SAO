using Godot;
using System;

public partial class Oxxy : Node2D
{
	[Export] private Unit me;
	[Export] private Sprite2D lLeaf, rLeaf;

	public override void _Ready()
	{
		me.OnArmorChange += OnArmorChange;
	}

	private void OnArmorChange(float arg1, float arg2)
	{
		var ratio = 100 * arg1 / me.Stats.Armor + me.Stats.ArmorMultiplier;
		if (ratio < 66)
			rLeaf.Visible = false;
		else if (ratio < 33) {
			lLeaf.Visible = false;
			me.OnArmorChange -= OnArmorChange;
		}
	}
}
