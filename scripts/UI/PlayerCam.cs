using Godot;
using System;

public partial class PlayerCam : Camera2D
{
	[Export] public float EdgeMargin { get; set; } = 50.0f; 
    [Export] public float MaxOffset { get; set; } = 50.0f;  
    [Export] public float SmoothSpeed { get; set; } = 8.0f; 

    public override void _Process(double delta)
    {
        Vector2 mousePos = GetViewport().GetMousePosition();
        Vector2 viewportSize = GetViewportRect().Size;

        float targetOffsetX = 0.0f;
        float targetOffsetY = 0.0f;

        if (mousePos.X < EdgeMargin) {
            float intensity = 1.0f - (mousePos.X / EdgeMargin);
            targetOffsetX = -MaxOffset * intensity; 
        }
        else if (mousePos.X > viewportSize.X - EdgeMargin) {
            float distanceToEdge = viewportSize.X - mousePos.X;
            float intensity = 1.0f - (distanceToEdge / EdgeMargin);
            targetOffsetX = MaxOffset * intensity;
        }
        if (mousePos.Y < EdgeMargin) {
            float intensity = 1.0f - (mousePos.Y / EdgeMargin);
            targetOffsetY = -MaxOffset * intensity;
        }else if (mousePos.Y > viewportSize.Y - EdgeMargin) {
            float distanceToEdge = viewportSize.Y - mousePos.Y;
            float intensity = 1.0f - (distanceToEdge / EdgeMargin);
            targetOffsetY = MaxOffset * intensity;
        }

        Vector2 targetOffset = new Vector2(targetOffsetX, targetOffsetY);

        Offset = Offset.Lerp(targetOffset, (float)(SmoothSpeed * delta));
    }
}
