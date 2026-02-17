using Godot;

[GlobalClass]
public partial class UnitResource : Resource
{
    [ExportGroup("unit stats")]
    [Export] public float Hp = 0;
    [Export] public float Strength = 0;
    [Export] public float Mp = 0;
    [Export] public float Speed = 0;
    [Export] public float Armor = 0;
    [Export] public float Intellegence = 0;
    [Export] public float KritMultiplier = 0;
    [Export] public float KritChance = 0;
    
    [ExportGroup("Stat Multipliers")]
    [Export] public float HpMultiplier = 1.0f;
    [Export] public float StrengthMultiplier = 1.0f;
    [Export] public float MpMultiplier = 1.0f;
    [Export] public float SpeedMultiplier = 1.0f;
    [Export] public float ArmorMultiplier = 1.0f;
    [Export] public float IntellegenceMultiplier = 1.0f;
    [Export] public float XpGainMultiplier = 1.0f;

    public void AddStat(UnitResource stat)
    {
        Hp += stat.Hp;
        Strength += stat.Strength;
        Mp += stat.Mp;
        Speed += stat.Speed;
        Armor += stat.Armor;
        Intellegence += stat.Intellegence;
        KritMultiplier += stat.KritMultiplier;
        KritChance += stat.KritChance;
        
        HpMultiplier += (stat.HpMultiplier - 1.0f);
        StrengthMultiplier += (stat.StrengthMultiplier - 1.0f);
        MpMultiplier += (stat.MpMultiplier - 1.0f);
        SpeedMultiplier += (stat.SpeedMultiplier - 1.0f);
        ArmorMultiplier += (stat.ArmorMultiplier - 1.0f);
        IntellegenceMultiplier += (stat.IntellegenceMultiplier - 1.0f);
        XpGainMultiplier += (stat.XpGainMultiplier - 1.0f);
    }
}