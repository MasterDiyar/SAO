using Godot;
using System;
using SAO.scripts.bullets;

public partial class Burst : Bullet
{
    private AnimatedSprite2D as2D;
    public override void _Ready()
    {
        base._Ready();
        as2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        
        as2D.Play();
    }
}