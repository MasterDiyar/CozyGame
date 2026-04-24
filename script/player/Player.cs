using Godot;
using Testcase.script.effects;

namespace Testcase.script.player;

public partial class Player : Unit
{
    [Export]public CostResource CurrentResources;
    [Export] public Attack Attack;
    public BodyStructure Body;
    
    [Export] public float KritChance = 0.2f;
    [Export] public float KritMultiplier = 1.5f;
    [Export] public float VampirismChance = 0f;
    [Export] public float VampirismMultiplier = 0f;
    [Export] public float AbsoluteAbsorbtionChance = 0f;

    public override void TakeDamage(float damage)
    {
        if (GD.Randf() < AbsoluteAbsorbtionChance) return;
        base.TakeDamage(damage);
    }

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
    
    

    public override void _ExitTree()
    {
        GameManager.Instance.GoToMenu();
    }
}