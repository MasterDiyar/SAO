using Godot;
using System;
using SAO.scripts.upgrades;

public partial class Unit : CharacterBody2D
{
	[Export] UnitResource Stats;
	public float CurrentHealth = 0, CurrentMana = 0, CurrentArmor = 0;
	public override void _Ready()
	{
		CurrentHealth = Stats.Hp;
		CurrentArmor = Stats.Armor;
		ChildEnteredTree += UpdateStats;
	}

	public virtual void UpdateStats(Node node)
	{
		if (node is UpgradeNode un)
		{
			Stats.AddStat(un.AddUpgrade());
		}
	}

	public void TakeDamage(float damage)
	{
		if (CurrentArmor > 0)
			CurrentArmor -= damage;
		else
			CurrentHealth -= damage;
		
		if (CurrentHealth <= 0)
			DefferedDie();
	}

	public void DefferedDie()
	{
		CallDeferred("queue_free");
	}
	
	
}
