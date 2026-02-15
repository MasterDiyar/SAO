namespace SAO.scripts.weapons;

using Godot;
using System.Collections.Generic;
using SAO.scripts.bullets;

public partial class BulletSpawner : Node
{
    public virtual void Spawn(List<Bullet> bullets)
    {
        foreach (var bt in bullets)
        {
            GetTree().Root.AddChild(bt); 
        }
    }
}