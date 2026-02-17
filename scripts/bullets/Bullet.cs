using Godot;

namespace SAO.scripts.bullets;

public partial class Bullet : Area2D
{
    [Export] public float Damage = 1;
    [Export] public float Speed = 1;
    [Export] public float AngularVelocity = 0;
    [Export] public float LifeTime = 10;
    public Node Owner;

    public override void _Ready()
    {
        BodyEntered += Horosh;
    }

    public void Horosh(Node2D h)
    {
        if (h is not Unit uit) return;
        if (uit == Owner)return;
        uit.TakeDamage(Damage);
    }

    public override void _Process(double delta) //for angulra speed = a = wR
    {
        float dl = (float) delta;
        LifeTime -= dl;
        if (LifeTime <= 0) QueueFree();
        Rotation += AngularVelocity * dl;
        Position += Speed * dl * Vector2.FromAngle(Rotation);
    }
}