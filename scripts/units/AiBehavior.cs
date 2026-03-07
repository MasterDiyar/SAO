using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using SAO.scripts.weapons;

public partial class AiBehavior : Node2D
{
	[Export] public int Escalation = 0;
	[Export] public bool CanWalk = false;
	private Player player;
	private Unit parent;
	public int NowB = 0;
	
	RandomNumberGenerator rng = new RandomNumberGenerator();
	
	List<WeaponTrigger> weapons = [];
	Timer timer;
	public override void _Ready()
	{
		parent = GetParent<Unit>();
		rng.Randomize();
		timer = new Timer();
		AddChild(timer);
		timer.WaitTime = 1.0f;
		timer.OneShot = false;
		timer.Autostart = true;
		timer.Timeout += GetBehavior;
		timer.Start();
		SetProcess(false);
		player = GetTree().GetFirstNodeInGroup("player") as Player;

		if (WeaponCheck())
			GD.Print("Alles is gut aib loaded correctly.");
	}

	void GetBehavior()
	{
		NowB = rng.RandiRange(0, 4);
		switch (NowB)
		{
			case 0:
				if (CanWalk)
					SetProcess(true);
				break;
			case 1:
				SetProcess(false);
				break;
			case 2:
				return;
		}
		
		if (!IsInstanceValid(player))
		{
			QueueFree();
			return;
		}
		
		if (player.GlobalPosition.DistanceSquaredTo(GlobalPosition) <= 40000 + Escalation * Escalation * 2500) {
			foreach (var weapon in weapons)
				weapon.RequestAttack(( player.GlobalPosition - GlobalPosition).Angle());
			
		}
	}


	bool WeaponCheck()
	{
		weapons = parent
			.GetChildren()
			.SelectMany(child => child
				.GetChildren()
				.OfType<WeaponTrigger>())
			.ToList();
			
		return weapons.Count > 0;
	}
	
	public  override void _Process(double delta)
	{
		Position += (float)delta * (player.GlobalPosition - GlobalPosition).Normalized() * parent.Stats.Speed;
	}
}
