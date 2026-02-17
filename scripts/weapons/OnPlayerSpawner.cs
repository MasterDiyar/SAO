using System.Collections.Generic;
using Godot;
using SAO.scripts.bullets;

namespace SAO.scripts.weapons;

public partial class OnPlayerSpawner : BulletSpawner
{
    public override void Spawn(List<Bullet> bullets, Node owner)
    {
        foreach (var bt in bullets)
        {
            bt.Owner = owner;
            GetParent().GetParent().AddChild(bt);
        }
    }
}