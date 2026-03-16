using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using SAO.scripts.weapons;

public partial class AiBehavior : Node2D
{
	[Export] public int Escalation = 0;
	[Export] public bool CanWalk = false;
	[Export] float StopDistance = 20f;
	private Player player;
	private Unit parent;
	public int NowB = 0;

	private Texture2D plus = GD.Load<Texture2D>("res://assets/unitas/plus.png");

	private CpuParticles2D cp2d;
	
	RandomNumberGenerator rng = new RandomNumberGenerator();
	
	[Signal] public delegate void ChangeBehaviorEventHandler(int behavior); //0-walk 1-aggressive 2-idling
	
	List<WeaponTrigger> weapons = [];
	Timer timer;
	public override void _Ready()
	{
		parent = GetParent<Unit>();
		cp2d = parent.GetNode<CpuParticles2D>("CPUParticles2D");
		
		rng.Randomize();
		
		timer = new Timer();
		AddChild(timer);
		timer.WaitTime = 1.0f;
		timer.OneShot = false;
		timer.Autostart = true;
		timer.Timeout += GetBehavior;
		timer.Start();
		
		SetProcess(CanWalk);
		
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
				if (CanWalk) {
					SetProcess(true);
					EmitSignal(nameof(ChangeBehavior), 0);
				}
				break;
			case 1:
				SetProcess(false);
				EmitSignal(nameof(ChangeBehavior), 2);
				break;
			case 2:
				var stats = parent.Stats;
				float maxArmor = stats.Armor * stats.ArmorMultiplier;

				if (parent.CurrentArmor < maxArmor) {
					float healAmount = stats.Strength + stats.StrengthMultiplier;
        
					parent.CurrentArmor = Mathf.Min(parent.CurrentArmor + healAmount, maxArmor);

					cp2d.Texture = plus;
					cp2d.Emitting = true;
				}
				EmitSignal(nameof(ChangeBehavior), 3);
				return;
		}
		
		if (!IsInstanceValid(player)) {
			QueueFree();
			return;
		}
		
		if (player.GlobalPosition.DistanceSquaredTo(GlobalPosition) <= 40000 + Escalation * Escalation * 2500) {
			EmitSignal(nameof(ChangeBehavior), 1);
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
		if (!IsInstanceValid(player)) {
			SetProcess(false);
			return;
		}
		float distSq = player.GlobalPosition.DistanceSquaredTo(GlobalPosition);

		if (!(distSq > StopDistance * StopDistance)) return;
		
		Vector2 direction = (player.GlobalPosition - GlobalPosition).Normalized();
		parent.LookAt(player.GetGlobalPosition());
		parent.Position += direction * parent.Stats.Speed * (float)delta;
	}
}