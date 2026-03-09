using System.Linq;
using Godot;

namespace SAO.scripts.weapons;

public abstract partial class BaseWeapon : Node2D
{
    [Export] public  WeaponTrigger Trigger;
    [Export] public int damageType = 1; //1 - projectile, 2 - magic, 3 - melee
    protected Unit daddy;
    
    public override void _Ready()
    {
        Trigger ??= GetChildren().OfType<WeaponTrigger>().FirstOrDefault();
        Trigger.ShootRequested += OnShootRequested;
        daddy = GetParent<Unit>();
    }

    protected abstract void OnShootRequested(float angle);
}