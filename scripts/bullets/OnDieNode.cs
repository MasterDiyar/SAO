using Godot;

namespace SAO.scripts.bullets;

public partial class OnDieNode : Node2D
{
    [Export] private int _type = 0;
    [Export] private int[] BufferNums = [0, 0, 0, 0];

    public override void _Ready()
    {
        switch (GetParent())
        {
            case Unit uit:
                uit.OnHealthChange += (o, e) =>
                {
                    if (o < 0) DieDieDie();
                };
                break;
            case Bullet bullet:
                bullet.Die += DieDieDie;
                break;
        }
    }

    public void DieDieDie()
    {
        Bullet bullet;
        PackedScene bulletS;
        GD.PrintRich("OH NO IM DYINGGG");
        switch (_type) {
            case 0:
                 bulletS = GD.Load<PackedScene>("res://scenes/bullet/bloda.tscn");
                
                for (int i = 0; i < BufferNums[0]; i++) {
                    bullet = bulletS.Instantiate<Bullet>();
                    bullet.Position = GlobalPosition;
                    bullet.Rotation = GD.Randf() * float.Tau;
                    bullet.OwnerA = GetOwnerA();
                    GetTree().Root.CallDeferred("add_child", bullet);
                } 
                break;
            case 1:
                bulletS = GD.Load<PackedScene>("res://scenes/bullet/miron.tscn");
                bullet = bulletS.Instantiate<Bullet>();
                bullet.Position = GlobalPosition;
                bullet.OwnerA = GetOwnerA();
                GetTree().Root.CallDeferred("add_child", bullet);
                break;
        }
    }

    Node GetOwnerA() => GetParent() switch
    {
        Unit   uit   => uit,
        Bullet bulle => bulle.OwnerA,
        _ => null
    };
    
    public override void _ExitTree()
    {
        if (GetParent() is Bullet bullet)
            bullet.Die -= DieDieDie;
    }
}