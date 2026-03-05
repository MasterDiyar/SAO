using Godot;

namespace SAO.scripts.bullets;

public partial class OnDieNode : Node2D
{
    [Export] private int _type = 0;
    [Export] private int[] BufferNums = [0, 0, 0, 0];

    public override void _ExitTree()
    {
        Bullet bullet;
        
        switch (_type) {
            case 0:
                PackedScene bulletS = GD.Load<PackedScene>("res://scenes/bullet/bloda.tscn");
                
                for (int i = 0; i < BufferNums[0]; i++) {
                    bullet = bulletS.Instantiate<Bullet>();
                    bullet.Position = GlobalPosition;
                    bullet.Rotation = GD.Randf() * float.Tau;
                    GetTree().Root.CallDeferred("add_child", bullet);
                } 
                break;
            case 1:
                break;
        }
    }
}