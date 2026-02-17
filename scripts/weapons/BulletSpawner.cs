namespace SAO.scripts.weapons;

using Godot;
using System.Collections.Generic;
using SAO.scripts.bullets;

public partial class BulletSpawner : Node
{
    public virtual void Spawn(List<Bullet> bullets, Node owner)
    {
        foreach (var bt in bullets)
        {
            bt.Owner = owner;
            GetTree().Root.AddChild(bt); 
        }
    }
}