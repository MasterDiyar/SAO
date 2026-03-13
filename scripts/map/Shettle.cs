using Godot;
using System;

public partial class Shettle : StaticBody2D
{
	[Export] private Timer blink, energyfall;
	[Export] private PointLight2D blinkLight, energyfallLight;
	public override void _Ready()
	{
		blink.Timeout += BlinkOnTimeout;
		energyfall.Timeout += EnergyfallOnTimeout;
	}

	private float currentEnergy = 0, currentTime = 0, maxEnergy = 2, minEnergy = 0.8f;
	private void EnergyfallOnTimeout()
	{
		throw new NotImplementedException();
	}

	private void BlinkOnTimeout()
	{
		throw new NotImplementedException();
	}


	public override void _Process(double delta)
	{
	}
}
