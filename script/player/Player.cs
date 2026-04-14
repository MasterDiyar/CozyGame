using Godot;
using Testcase.script.effects;

namespace Testcase.script.player;

public partial class Player : Unit
{
    [Export]public CostResource CurrentResources;
    [Export] public Attack Attack;
    public BodyStructure Body;

    public override void _Ready()
    {
        base._Ready();
        Body = GetNode<BodyStructure>("BodyNode");
        
    }

    public override void _Input(InputEvent vent)
    {
        if (vent.IsActionPressed("lm"))
        {
            var attackAngle = (GetGlobalMousePosition()- GlobalPosition).Angle();
            Attack.ExecuteAttack(attackAngle, this);
        }
    }
}