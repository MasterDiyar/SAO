namespace SAO.scripts.weapons;

using Godot;
using System;
using System.Collections.Generic;
using bullets; 

public partial class BulletFactory : Node2D
{
    [Export] public PackedScene BulletScene;
    [Export] public int   Count        = 1; 
    [Export] public float BetweenAngle = 0; 
    [Export] public float BulletScale  = 1; 
    [Export] public float Offset       = 0;
    [Export] public float AngleOffset  = 0;
    
    [ExportGroup("Extras")]
    [Export] private float randomAngleModifier = 0;
    [Export] private float randomSpeedModifier = 0;

    public virtual List<Bullet> CreateBullets(Vector2 originPosition, float baseAngle)
    {
        var bullets = new List<Bullet>();
        
        float angle = baseAngle; 

        for (int i = 0; i < Count; i++)
        {
            if (BulletScene == null) continue;

            Bullet bt = BulletScene.Instantiate<Bullet>();
            
            var randomAngle = (GD.Randf() - 0.5f) * 2 * randomAngleModifier;
            var randomSpeed = (GD.Randf() - 0.5f) * 2 * randomSpeedModifier;
            
            bt.Speed += randomSpeed;
            bt.Rotation = AngleOffset + angle + BetweenAngle * i + randomAngle; 
            bt.GlobalPosition = originPosition + Vector2.FromAngle(angle+ BetweenAngle * i) * Offset;
            bt.GlobalScale *= BulletScale;

            bullets.Add(bt);
        }
        
        return bullets;
    }
} 