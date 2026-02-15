namespace SAO.scripts.weapons;

using Godot;
using System;

public partial class WeaponTrigger : Node
{
    public Action<float> ShootRequested;
    

    [Export] public float AttackSpeed = 1; 
    [Export] public bool AutoAttack = false; 

    private float _co = 0;
    private bool canShoot = false;

    public override void _Process(double delta)
    {
        _co += (float)delta; 

        if (AutoAttack)
        {
            if (!(_co > AttackSpeed)) return;
            _co = 0;

            ShootRequested?.Invoke(0);
        }
        else
            canShoot = _co > AttackSpeed;
        
    }

    internal void RequestAttack(float angle)
    {
        if (!canShoot) return;
        ShootRequested?.Invoke(angle);
        _co = 0;
    }
}