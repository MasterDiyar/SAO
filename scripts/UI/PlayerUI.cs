using Godot;
using System;

public partial class PlayerUI : CanvasLayer
{
	[Export] private Player pl;

	private ProgressBar pB;
	private Label HpLabel;

	private float Hp, Armor;
	
	public override void _Ready()
	{
		pl.XpGained += XpGained;
		pl.OnHealthChange += OnHealthChange;
		pl.OnArmorChange += OnArmorChange;
		Hp = pl.Stats.Hp;
		Armor = pl.Stats.Armor;
		pB = GetNode<ProgressBar>("Control/ProgressBar");
		HpLabel = GetNode<Label>("Control/Hp");
	}

	private void OnHealthChange(float arg1, float arg2)
	{
		Hp = arg1;
		HpLabel.Text = "Hp: " +Hp.ToString("0.0")+"\nArmor: "+Armor.ToString("0.0");
	}

	private void OnArmorChange(float arg1, float arg2)
	{
		Armor = arg1;
		HpLabel.Text = "Hp: " +Hp.ToString("0.0") + "\nArmor: " +Armor.ToString("0.0");
	}

	private void XpGained()
	{
		pB.Value = pl.Xp;
		pB.MaxValue = pl.XpToNextLevel;
	}

	
}
