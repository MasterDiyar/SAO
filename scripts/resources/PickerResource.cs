using Godot;

[GlobalClass]
public partial class PickerResource : Resource
{

    [Export] private PackedScene pickCard;
    [Export] private int Level = 0;
    [Export] int MaxLevel = 0;
    [Export] bool Repeatable = false;

}