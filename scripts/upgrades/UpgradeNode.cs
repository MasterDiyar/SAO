using Godot;

namespace SAO.scripts.upgrades;

public partial class UpgradeNode : Node
{
    [Export] UnitResource resource;

    public virtual UnitResource AddUpgrade()
    {
        return resource;
    }
}