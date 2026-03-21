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
		if (level-1>UpgradeTextures.Length) return; 
		Numero.GetParent<TextureRect>().Texture = UpgradeTextures[int.Clamp(level ,0,UpgradeTextures.Length-1)];
	}
	
	public override void _GuiInput(InputEvent @event)
	{
		if (@event is not InputEventMouseButton { ButtonIndex: MouseButton.Left, Pressed: true }) return;
		ApplyUpgradeToPlayer();
		isPicked?.Invoke();
	}

	protected virtual void ApplyUpgradeToPlayer()
	{
		if (GetTree().GetFirstNodeInGroup("player") is not Unit player) {
			GD.PrintErr("Ошибка: Объект игрока с группой 'Player' не найден!");
			return;
		}
		
		int index = Math.Clamp(level, 0, UpgradeTexts.Length - 1);
		
		if (index >= UpgradeResources.Length) {
			GD.PrintErr("Ошибка: Для текущего уровня нет соответствующего UnitResource!", index);
			return;
		}
		UnitResource upgrade = UpgradeResources[index];	
		
		if (upgrade == null) {
			GD.PrintErr($"Ошибка: Ресурс по индексу {index} равен null!");
			return;
		}

		if (player.Stats == null) {
			GD.PrintErr("Ошибка: У игрока не инициализирован компонент Stats!");
			return;
		}
		player.Stats.AddStat(upgrade);
		
	}

	public void UpdateText(int lvl)
	{
		level = lvl;
		
		if (UpgradeTexts == null || UpgradeTexts.Length == 0) {
			Numero.Update(level);
			return;
		}
		
		int safeIndex = Math.Clamp(level , 0, UpgradeTexts.Length - 1);
		string rawText = UpgradeTexts[safeIndex];

		if (string.IsNullOrEmpty(rawText)) {
			Numero.Update(level);
			return;
		}
		string[] texts = rawText.Split(';');

		if (texts.Length >= 2) {
			Plus.Text = texts[0];
			Minus.Text = texts[1];
		}else Plus.Text = rawText;
		
		Numero.Update(level);
	}
}
