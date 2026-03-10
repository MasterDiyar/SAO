using Godot;
using System;
using System.Collections.Generic;
using SAO.scripts.weapons;

public partial class RedRoom : BaseWeapon
{
    [Export] private Sprite2D sword;
    [Export] private Area2D swordArea;
    [Export] private Line2D lien;
    [Export] private float SpeedMultiplier = 10f;
    [Export] private float FlyDuration = 0.4f;

    public Action OneCycle;

    private bool isFlying = false;
    private bool returning = false;
    private float t = 0f;
    private Vector2 targetPosition;

    public override void _Ready()
    {
        base._Ready();
        swordArea.BodyEntered += OnSwordAreaEntered;
    }

    public override void _Process(double delta)
    {
        if (!isFlying) {
            sword.Position = Vector2.Zero;
            sword.Rotation = (GetGlobalMousePosition() - GlobalPosition).Angle();
            lien.ClearPoints();
            return;
        }

        float dt = (float)delta;
        t += dt / FlyDuration;

        if (!returning) {
            float easeT = 1.0f - (1.0f - t) * (1.0f - t);
            sword.Position = Vector2.Zero.Lerp(targetPosition, easeT);

            if (t >= 1.0f) {
                t = 0;
                returning = true;
                OneCycle?.Invoke();
            }
        }else {
            float easeT = t * t * t;
            sword.Position = targetPosition.Lerp(Vector2.Zero, easeT);
            if (t >= 1.0f)
                isFlying = false;
        }

        if (lien.GetPointCount() < 2) {
            lien.AddPoint(Vector2.Zero);
            lien.AddPoint(sword.Position);
        }else {
            lien.SetPointPosition(0, Vector2.Zero);
            lien.SetPointPosition(1, sword.Position);
        }
    }

    protected override void OnShootRequested(float angle)
    {
        if (isFlying) return;

        isFlying = true;
        returning = false;
        t = 0;

        float distance = daddy.Stats.Strength * SpeedMultiplier / 2;
        targetPosition = Vector2.FromAngle(angle) * distance;
        sword.Rotation = angle;

        lien.ClearPoints();
        lien.AddPoint(Vector2.Zero);
        lien.AddPoint(Vector2.Zero);
    }

    private void OnSwordAreaEntered(Node2D body)
    {
        if (!isFlying) return;
        if (body is not Unit unit || unit == daddy) return;
        unit.TakeDamage(((GD.Randf() < daddy.Stats.KritChance) ? daddy.Stats.KritMultiplier : 1)
            * daddy.Stats.Strength * SpeedMultiplier / 20);
    }
}
