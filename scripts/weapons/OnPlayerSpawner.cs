using System.Collections.Generic;
using SAO.scripts.bullets;

namespace SAO.scripts.weapons;

public partial class OnPlayerSpawner : BulletSpawner
{
    public override void Spawn(List<Bullet> bullets)
    {
        foreach (var VARIABLE in bullets)
        {
            GetParent().GetParent().AddChild(VARIABLE);
        }
    }
}