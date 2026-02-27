using Godot;
using System;
using System.Collections.Generic;

public partial class CardPicker : Control
{
    [Export] private Button ExitButton;
    [ExportGroup("cards")] [Export] private PickerResource[] allCards;
    [Export] private int cardsToShow = 3;

    HBoxContainer HBoxContainer;
    RandomNumberGenerator rng = new RandomNumberGenerator();

    public override void _Ready()
    {
        HBoxContainer = GetNode<HBoxContainer>("MarginContainer/HBoxContainer");
        ExitButton.Pressed += () => Toggle(false);
        rng.Randomize();
    }

    public void Shuffle()
    {
        if (allCards == null || allCards.Length == 0) return;

        List<PickerResource> pool = [];

        foreach (var card in allCards)
        {
            if (card == null || card.pickCard == null) continue;

            bool canAppear = card.Repeatable || card.Level < card.MaxLevel;
            if (canAppear)
                pool.Add(card);
        }

        if (pool.Count == 0) return;

        // Fisher–Yates shuffle
        for (int i = pool.Count - 1; i > 0; i--) {
            int j = rng.RandiRange(0, i);
            (pool[i], pool[j]) = (pool[j], pool[i]);
        }

        int spawnCount = Mathf.Min(cardsToShow, pool.Count);

        for (int i = 0; i < spawnCount; i++)
        {
            var data = pool[i];
            var cardNode = data.pickCard.Instantiate<UpgradeCard>();
            cardNode.UpdateText(data.Level);

            cardNode.isPicked += () => {
                data.Level++;
                Toggle(false);
            };

            HBoxContainer.AddChild(cardNode);
        }
    }


    public void Toggle(bool enable = true)
    {
        Visible = enable;
        if (!enable)
        {
            ClearContainer();
            return;
        }

        ClearContainer();
        Shuffle();
    }
    
    void ClearContainer()
    {
        foreach (Node child in HBoxContainer.GetChildren())
            child.QueueFree();
    }
}