using Godot;
using System;

public partial class DebugNode : Node2D
{
	[Export] private OptionButton btns;
	[Export] private PackedScene[] mobs;
	RandomNumberGenerator rng = new RandomNumberGenerator();
	public override void _Ready()
	{
		rng.Randomize();
		btns.Pressed += pressed;
	}

	void pressed()
	{
		switch (btns.Selected)
		{
			case 0:
				var a = ((Player)GetTree().GetFirstNodeInGroup("player"));
				a.AddXp(100);
				break;
			
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
