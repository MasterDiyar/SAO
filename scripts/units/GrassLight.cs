using Godot;

public partial class GrassLight : PointLight2D
{
	[Export] public float Speed { get; set; } = 2.0f;
	[Export] public float Amplitude { get; set; } = 20.0f;
	[Export] public float Strength { get; set; } = 1.0f; 

	private float _startX;

	public override void _Ready()
	{
		_startX = Position.X;
	}

	public override void _Process(double delta)
	{
		double time = Time.GetTicksMsec() / 1000.0;
		float offsetX = Mathf.Sin((float)time * Speed) * Amplitude * Mathf.Pow(Strength, 2);
		Position = new Vector2(_startX + offsetX, Position.Y);
	}
}
