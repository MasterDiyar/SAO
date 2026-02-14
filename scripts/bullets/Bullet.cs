using Godot;

namespace SAO.scripts.bullets;

public partial class Bullet : Area2D
{
    [Export] public float Damage = 1;
    [Export] public float Speed = 1;
    [Export] public float AngularVelocity = 0;

    public override void _Ready()
    {
        BodyEntered += Horosh;
    }

    public void Horosh(Node2D h)
    {
        if (h is Unit uit)
        {
            uit.TakeDamage(Damage);
        }
    }

    public override void _Process(double delta)
    {
        float dl = (float) delta;
        Rotation += AngularVelocity * dl;
        Position += Speed * dl * Vector2.FromAngle(Rotation);
    }
}