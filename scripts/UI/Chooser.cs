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
		rightUnitPick.Pressed += RUP;
	}

	void LUP()
	{
		unitIndex = (unitIndex + 5) % 3;
		UnitIndexChanged();
	}

	void RUP()
	{
		unitIndex = (unitIndex + 1) % 3;
		UnitIndexChanged();
	}

	void UnitIndexChanged()
	{
		var unitIO = unitIndex % 3;
		switch (unitIO)
		{
			case 0:
				unitTexture.Texture = GD.Load<Texture2D>("res://assets/unitas/Ufonat.png");
				break;
			case 1:
				unitTexture.Texture = GD.Load<Texture2D>("res://assets/unitas/Mercator.png");
				break;
			case 2:
				var at2D = new AtlasTexture();
				at2D.Atlas = GD.Load<Texture2D>("res://assets/unitas/Dupont.png");
				at2D.Region = new Rect2(112, 8, 64, 72);
				unitTexture.Texture = at2D;
				break;
		}
	}
	
	public override void _Ready()
	{
		PlayButton.Pressed += PlayButtonOnPressed;
		
		UnitChooserInit();
	}

	private void PlayButtonOnPressed()
	{
		string[] pScenes = ["res://scenes/units/player.tscn", "res://scenes/units/shemitir.tscn", "res://scenes/units/duponte.tscn"];
		var map = GD.Load<PackedScene>("res://scenes/maps/map.tscn").Instantiate();
		Player player = GD.Load<PackedScene>(pScenes[unitIndex%3]).Instantiate<Player>();
		
		PointLight2D pl2d = new PointLight2D();
		pl2d.Texture = GD.Load<Texture2D>("res://assets/unitas/crimlight.png");
		pl2d.Energy = 1.56f;
		player.AddChild(pl2d);
		
		GetTree().Root.AddChild(map);
		map.AddChild(player);
		QueueFree();
	}
}
