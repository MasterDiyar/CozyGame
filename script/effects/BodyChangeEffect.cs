using Godot;
using Testcase.script.player;


[GlobalClass] public partial class BodyChangeEffect : Effect
{
    [Export] private BodyStructure.BodyPart part;
    [Export] private Texture2D texture;
    public override void Apply(Unit unit)
    {
        if (unit is not Player pl) return;
        pl.GetNode<BodyStructure>("BodyNode").ChangePart(part,  texture);
        
    }
}