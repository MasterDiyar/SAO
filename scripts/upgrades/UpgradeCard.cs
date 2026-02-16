using Godot;
using System;

public partial class UpgradeCard : Panel
{
	[Export] public int level= 1;
	[Export] public string[] UpgradeTexts = [];
	[Export] public UnitResource[] UpgradeResources = [];
	private Label Plus, Minus;
	private UpgradeNum Numero;
	public override void _Ready()
	{
		Plus  = GetNode<Label>("VBoxContainer/Plus");
		Minus = GetNode<Label>("VBoxContainer/Minus");
		Numero = GetNode<UpgradeNum>("VBoxContainer/Texture/TextureRect");
	}
	
	public override void _GuiInput(InputEvent @event)
	{
		if (@event is InputEventMouseButton { ButtonIndex: MouseButton.Left, Pressed: true })
			ApplyUpgradeToPlayer();
		
	}

	private void ApplyUpgradeToPlayer()
	{
		var player = GetTree().GetFirstNodeInGroup("Player") as Unit;

		if (player != null) {
			int index = level - 1;
			if (index >= 0 && index < UpgradeResources.Length) {
				UnitResource upgrade = UpgradeResources[index];
				if (player.Stats != null && upgrade != null)
					player.Stats.AddStat(upgrade);
				
			}else 
				GD.PrintErr("Ошибка: Для текущего уровня нет соответствующего UnitResource!");
		}
		else
			GD.PrintErr("Ошибка: Объект игрока с группой 'Player' не найден!");
	}

	public void UpdateText(int lvl)
	{
		level = lvl;
		string[] texts = UpgradeTexts[level-1].Split(';');
		Plus.Text = texts[0];
		Minus.Text = texts[1];
		Numero.Update(level);
	}
}
