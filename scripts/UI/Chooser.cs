using Godot;
using System;

public partial class Chooser : Control
{
	[Export] private Button PlayButton;
	[ExportGroup("Unit Choose")]
	[Export] TextureRect unitTexture;
	[Export] Button leftUnitPick, rightUnitPick;
	[Export] private Label unitInfo;
	int unitIndex = 0;
	
	void UnitChooserInit()
	{
		leftUnitPick.Pressed += LUP;
	}

	void LUP()
	{
		unitIndex--;
	}
	
	public override void _Ready()
	{
		PlayButton.Pressed += PlayButtonOnPressed;
		
		UnitChooserInit();
	}

	private void PlayButtonOnPressed()
	{
		var map = GD.Load<PackedScene>("res://scenes/maps/map.tscn").Instantiate();
		Player player = null;
		switch (unitIndex)
		{
			case 0:
				player = GD.Load<PackedScene>("res://scenes/units/player.tscn").Instantiate<Player>();
				
				break;
		}
		
		GetTree().Root.AddChild(map);
		map.AddChild(player);
		QueueFree();
	}
}
