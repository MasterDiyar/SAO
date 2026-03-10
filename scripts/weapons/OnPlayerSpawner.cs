using System.Collections.Generic;
using Godot;
using SAO.scripts.bullets;

namespace SAO.scripts.weapons;

public partial class OnPlayerSpawner : BulletSpawner
{
    public override void Spawn(List<Bullet> bullets, Node owner, float addedDamage, float kritMultiplier)
    {
        foreach (var bt in bullets)
        {
            bt.OwnerA = owner;
            bt.AddedDamage = addedDamage;
            bt.KritMultiplier = kritMultiplier;
            GetParent().GetParent().AddChild(bt);
        }
    }
}