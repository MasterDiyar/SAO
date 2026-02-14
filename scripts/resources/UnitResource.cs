using Godot;

[GlobalClass]
public partial class UnitResource : Resource
{
    [ExportGroup("unit stats")]
    [Export] public float Hp = 10;
    [Export] public float Strength = 10;
    [Export] public float Mp = float.MaxValue;
    [Export] public float Speed = 10;
    [Export] public float Armor = 10;
    [Export] public float Intellegence = 10;
    [Export] public float KritMultiplier = 1;
    [Export] public float KritChance = 10;

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
    }
}