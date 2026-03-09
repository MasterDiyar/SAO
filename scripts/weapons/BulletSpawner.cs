namespace SAO.scripts.weapons;

using Godot;
using System.Collections.Generic;
using SAO.scripts.bullets;

public partial class BulletSpawner : Node
{
    public virtual void Spawn(List<Bullet> bullets, Node owner, float AddedDamage, float KritMultiplier)
    {
        foreach (var bt in bullets)
        {
            bt.OwnerA = owner;
            bt.AddedDamage = AddedDamage;
            bt.KritMultiplier = KritMultiplier;
            GetTree().Root.AddChild(bt); 
        }
    }
}