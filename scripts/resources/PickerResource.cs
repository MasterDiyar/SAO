using Godot;

[GlobalClass]
public partial class PickerResource : Resource
{

    [Export] public PackedScene pickCard;
    [Export] public int Level = 0;
    [Export] public int MaxLevel = 0;
    [Export] public bool Repeatable = false;

}