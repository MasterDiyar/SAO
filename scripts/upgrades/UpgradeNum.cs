using Godot;
using System;

public partial class UpgradeNum : TextureRect
{
	[Export] private int num = 1;
	private string[] nane = ["one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten"];
	public override void _Ready()
	{
		num = GetParent().GetParent().GetParent<UpgradeCard>().level;
		Texture = GD.Load<Texture2D>("res://assets/upgrades/"+nane[num]+".png");
	}
}
