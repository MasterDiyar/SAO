using System;
using Godot;

namespace SAO.scripts.bullets;

public partial class Bullet : Area2D
{
    [Export] public float Damage = 1;
    [Export] public float Speed = 1;
    [Export] public float AngularVelocity = 0;
    [Export] public float LifeTime = 10;
    [Export] public float OnTouchConsume = 1;
    public float AddedDamage = 0, KritMultiplier = 1;
    public Node OwnerA;
    public Action Die;

    public override void _Ready()
    {
        BodyEntered += Horosh;
        Die += QueueFree;
    }

    public void Horosh(Node2D h)
    {
        if (h is not Unit uit) return;
        if (uit == OwnerA)     return;
        uit.TakeDamage((Damage + AddedDamage) * KritMultiplier);
        LifeTime -= OnTouchConsume;
        
        if (LifeTime <= 0) {
            InvokeDie();
            SetDeferred("monitoring", false); 
        }
    }

    public override void _Process(double delta) //for angulra speed = a = wR
    {
        float dl = (float) delta;
        LifeTime -= dl;
        if (LifeTime <= 0) InvokeDie();
        Rotation += AngularVelocity * dl;
        Position += Speed * dl * Vector2.FromAngle(Rotation);
    }
    
    private bool _isDead = false;

    public void InvokeDie()
    {
        if (_isDead) return;
        _isDead = true;
        Die?.Invoke();
        QueueFree();
    }
}