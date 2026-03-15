namespace SAO.scripts.weapons;

using Godot;
using System;

public partial class WeaponTrigger : Node
{
    public Action<float> ShootRequested;
    

    [Export] public float AttackSpeed = 1; 
    [Export] public bool AutoAttack = false; 

    protected float _co = 0;
    private bool canShoot = false;

    public override void _Process(double delta)
    {
        _co += (float)delta; 

        if (AutoAttack)
        {
            if (!(_co > AttackSpeed)) return;
            _co = 0;

            ExecuteAttack(0);
        }
        else
            canShoot = _co > AttackSpeed;
        
    }

    internal virtual void RequestAttack(float angle)
    {
        if (!canShoot) return;
        ExecuteAttack(angle);
        _co = 0;
    }
    
    protected virtual void ExecuteAttack(float angle)
    {
        ShootRequested?.Invoke(angle);
    }
}