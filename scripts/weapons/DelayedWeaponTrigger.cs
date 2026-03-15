namespace SAO.scripts.weapons;

using Godot;
using System;

public partial class DelayedWeaponTrigger : WeaponTrigger
{
    [Export] public float WindupTime = 0.5f; 

    protected override async void ExecuteAttack(float angle)
    {
        if (WindupTime <= 0)
        {
            base.ExecuteAttack(angle);
            return;
        }

        await ToSignal(GetTree().CreateTimer(WindupTime), SceneTreeTimer.SignalName.Timeout);

        base.ExecuteAttack(angle);
    }
}