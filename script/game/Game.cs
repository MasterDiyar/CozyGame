using Godot;
using System;
using Testcase.script.player;

public partial class Game : Node2D
{
    public void PickedType(int type)
    {
        var a =GetNode<UpgradeTree>("UI/UpgradeTree");
        var b = (new[] {"res://scene/upgrade_tree/knifer.tscn", "res://scene/upgrade_tree/"})[type];
        a.AddToTree(GD.Load<PackedScene>(b));
    }
    public override void _Ready()
    {
        GameManager.Instance.Game = this;
        GameManager.Instance.Pausable = GetNode<Node2D>("Pausable");
        GameManager.Instance.UI = GetNode<CanvasLayer>("UI");
        GameManager.Instance.Player = GetNode<Player>("Pausable/map/Player");
    }
}
