using System.Collections.Generic;
using Godot;
using SAO.scripts.bullets;

namespace SAO.scripts.weapons;

public partial class MultiNodeAddSpawner : BulletSpawner
{
    [Export] private PackedScene[] scenes;
    
    public virtual void Spawn(List<Bullet> bullets, Node owner)
    {
        foreach (var bt in bullets)
        {
            bt.OwnerA = owner;
            GetTree().Root.AddChild(bt); 
            foreach (var scene in scenes) bt.AddChild(scene.Instantiate());
        }
    }
}