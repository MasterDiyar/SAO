using Godot;
using System;
using Godot.Collections;

// Атрибут [Tool] заставляет скрипт работать прямо в редакторе Godot
[Tool] 
public partial class Grass : Node2D
{
    private Map map;
    private Player player;
    
    private Array<Rect2> _noSpawnRects = new Array<Rect2>();

    // Используем свойство, чтобы при изменении массива в инспекторе перерисовывать экран
    [Export] 
    public Array<Rect2> NoSpawnRects 
    { 
        get => _noSpawnRects;
        set 
        {
            _noSpawnRects = value;
            QueueRedraw(); // Даем команду Godot перерисовать узел
        }
    }

    public override void _Ready()
    {
        // В режиме редактора нам не нужно искать игрока и генерировать траву
        if (Engine.IsEditorHint()) return;

        map = GetParentOrNull<Map>();
        if (map != null)
        {
            map.PlayerFoundInvoker += (_player) => { player = _player; AssignPlayer(); };
        }
        Visible = true;
    }

    void AssignPlayer()
    {
        player ??= GetTree().GetFirstNodeInGroup("player") as Player;

        foreach (var child in GetChildren())
        {
            if (child is SmartGrass grass)
            {
                grass.Player = player;

                // Переводим координаты для SmartGrass (как обсуждали ранее)
                Array<Rect2> localizedRects = new Array<Rect2>();
                foreach (var worldRect in NoSpawnRects)
                {
                    Vector2 localPos = grass.ToLocal(ToGlobal(worldRect.Position));
                    Vector2 localSize = worldRect.Size * (GlobalScale / grass.GlobalScale); 
                    localizedRects.Add(new Rect2(localPos, localSize));
                }

                grass.NoSpawnRects = localizedRects;
                grass.Generate();
            }
        }
    }

    // Этот метод отвечает за отрисовку
    public override void _Draw()
    {
        // Рисуем только в редакторе или если включено отображение коллизий в отладке
        if (Engine.IsEditorHint() || GetTree().DebugCollisionsHint)
        {
            if (NoSpawnRects == null) return;

            // Настраиваем цвет: Красный, полупрозрачный (Альфа = 0.4)
            Color fillColor = new Color(1.0f, 0.0f, 0.0f, 0.4f);
            // Цвет обводки: Ярко-красный, непрозрачный
            Color outlineColor = new Color(1.0f, 0.0f, 0.0f, 1.0f);

            foreach (Rect2 rect in NoSpawnRects)
            {
                // Рисуем залитый прямоугольник
                DrawRect(rect, fillColor, true);
                // Рисуем рамку (толщина 2 пикселя)
                DrawRect(rect, outlineColor, false, 2.0f);
            }
        }
    }
}