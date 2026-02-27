using Godot;
using System;

public partial class UpgradeCard : Panel
{
	[Export] public int level= 1;
	[Export] public string[] UpgradeTexts = [];
	[Export] public UnitResource[] UpgradeResources = [];
	[ExportGroup("ImagePerLevel")]
	[Export] Texture2D[] UpgradeTextures;
	[Export] bool isItNeeded = false;
	[Export] private Label Plus, Minus;
	[Export] private UpgradeNum Numero;

	public Action isPicked;
	public override void _Ready()
	{
		if (!isItNeeded) return;
		Numero.GetParent<TextureRect>().Texture = UpgradeTextures[level-1];
	}
	
	public override void _GuiInput(InputEvent @event)
	{
		if (@event is not InputEventMouseButton { ButtonIndex: MouseButton.Left, Pressed: true }) return;
		ApplyUpgradeToPlayer();
		isPicked?.Invoke();
	}

	protected virtual void ApplyUpgradeToPlayer()
	{
		if (GetTree().GetFirstNodeInGroup("Player") is Unit player) {
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
		if (UpgradeTexts != null) {
			string[] texts = UpgradeTexts[level - 1].Split(';');
			if (texts.Length > 1) {
				Plus.Text = texts[0];
				Minus.Text = texts[1];
			}else {
				if (UpgradeTexts[level - 1] != null)
					Plus.Text = UpgradeTexts[level - 1];
			}
		}
		Numero.Update(level);
	}
}
