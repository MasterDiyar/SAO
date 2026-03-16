using System.Linq;

namespace SAO.scripts.weapons;
using Godot;

public partial class Swords : BaseWeapon
{
    [Export] private string nodePath;
    Area2D swordArea;

    public override void _Ready()
    {
        base._Ready();
        swordArea = GetNode<Area2D>(nodePath);
        swordArea.BodyEntered += OnSwordAreaEntered;
    }

    protected override void OnShootRequested(float angle)
    {
        
    }
    
    private void OnSwordAreaEntered(Node2D body)
    {
        if (body is not Unit unit || unit == daddy) return;
        unit.TakeDamage(((GD.Randf() < daddy.Stats.KritChance) ? daddy.Stats.KritMultiplier : 1)
            * daddy.Stats.Strength) ;
    }
}