using Godot;
using System;

public partial class AnimationExecuter : Node
{
	[Export] private AnimationPlayer apl;
	[Export] string AnimationName = "";
	public override void _Ready()
	{
		apl.Play(AnimationName);
	}
}
