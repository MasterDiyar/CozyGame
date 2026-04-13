using Godot;
using Testcase.script.effects;

namespace Testcase.script.player;

public partial class Player : Unit
{
    [Export]public CostResource CurrentResources;
    public Node2D Body;

    public override void _Ready()
    {
        base._Ready();
        Body = GetNode<Node2D>("Node2D");
        
    }
}