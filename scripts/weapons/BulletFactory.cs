namespace SAO.scripts.weapons;

using Godot;
using System;
using System.Collections.Generic;
using bullets; 

public partial class BulletFactory : Node2D
{
    [Export] public PackedScene BulletScene;
    [Export] public int Count = 1; 
    [Export] public float BetweenAngle = 0; 
    [Export] public float BulletScale = 1; 
    [Export] public float Offset = 0; 

    public virtual List<Bullet> CreateBullets(Vector2 originPosition, float baseAngle)
    {
        var bullets = new List<Bullet>();
        
        float angle = baseAngle; 

        for (int i = 0; i < Count; i++)
        {
            if (BulletScene == null) continue;

            Bullet bt = BulletScene.Instantiate<Bullet>();

            bt.Rotation = angle + BetweenAngle * i; 
            bt.GlobalPosition = originPosition + Vector2.FromAngle(angle+ BetweenAngle * i) * Offset;
            bt.GlobalScale *= BulletScale;

            bullets.Add(bt);
        }
        
        return bullets;
    }
}