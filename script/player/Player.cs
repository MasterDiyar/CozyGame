using Godot;
using Testcase.script.effects;

namespace Testcase.script.player;

public partial class Player : Unit
{
    [Export]public CostResource CurrentResources;
    public BodyStructure Body;

    public override void _Ready()
    {
        base._Ready();
        Body = GetNode<BodyStructure>("BodyNode");
        
    }
}