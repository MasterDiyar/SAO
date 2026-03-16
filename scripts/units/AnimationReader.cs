using Godot;

namespace SAO.scripts.units;

public partial class AnimationReader : Node2D
{
    [Export] AnimationPlayer mainPlayer, secondaryPlayer;
    [Export] AiBehavior behavior;
    [ExportGroup("MainProperties")] [Export]
    private bool HasIdle, HasMove, HasAttack, HasDie, HasHeal;
    [Export] string defaultAnimationName;

    [Export] private bool HasMultiIdle;
    [Export] private int count;
    
    [ExportGroup("SecondaryProperties")] [Export]
    private bool isBlinking, isSparkling, isShimmering;

    [Export] private Sprite2D[] Armors;

    private Unit Parent;

    [Export] private Node2D rotateNode;
    

    public override void _Ready()
    {
        Parent = GetParent<Unit>();
        
        
        Parent.OnArmorChange += OnArmorChange;
        behavior.ChangeBehavior += BehaviorOnChangeBehavior;
    }

    private void OnArmorChange(float arg1, float arg2)
    {
        for (int i = 0; i < Armors.Length; i++)
        {
            Armors[i].Visible = ((Parent.CurrentArmor / arg1) % Armors.Length > i);
        }
    }

    private void BehaviorOnChangeBehavior(int i)
    {
        mainPlayer.Play("RESET");
        switch (i) {
            case 0:
                if (HasMove) mainPlayer.Play("walk");
                break;
            case 1:
                if (HasAttack) mainPlayer.Play("attack");
                break;
            case 2:
                if (HasIdle) mainPlayer.Play("idle");
                break;
            case 3:
                if (HasHeal) mainPlayer.Play("heal");
                break;
            case 4:
                if (HasDie) mainPlayer.Play("death");
                break;
            default:
                mainPlayer.Play(defaultAnimationName);
                break;
        }
    }
}