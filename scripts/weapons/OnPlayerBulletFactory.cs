using System.Collections.Generic;
using Godot;
using SAO.scripts.bullets;

namespace SAO.scripts.weapons;

public partial class OnPlayerBulletFactory : BulletFactory
{
    
    public override List<Bullet> CreateBullets(Vector2 originPosition, float baseAngle)
    {
        var bullets = new List<Bullet>();
        
         

        for (int i = 0; i < Count; i++)
        {
            if (BulletScene == null) continue;

            Bullet bt = BulletScene.Instantiate<Bullet>();
            float angle = baseAngle + BetweenAngle * i;
            bt.Rotation = angle ; 
            bt.Position = Vector2.FromAngle(angle-Mathf.Pi/2) * Offset;
            bt.GlobalScale *= BulletScale;

            bullets.Add(bt);
        }
        
        return bullets;
    }
}