using Godot;

namespace SAO.scripts.units;

public partial class Exp : Area2D
{
    [Export] public float Count = 1;
    public override void _Ready()
    {
        BodyEntered += body => {
            if (body is not Player player) return;
            player.AddXp(Count);
            QueueFree();
        };
    }
}