using Godot;
using System;

public partial class CardPicker : Control
{
	[Export] private Button ExitButton;
	[ExportGroup("cards")] [Export] private PickerResource[] allCards;
	
	public override void _Ready()
	{
		ExitButton.Pressed += () => Toggle(false);
	}



	void Toggle(bool enable = true)
	{
		Visible = enable;
		if (enable)
		{
			//adding cards
		}
	}
}
