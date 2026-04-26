using Godot;
using System;
using Testcase.script.player;

public partial class Game : Node2D
{
    private int _savedType = -1;
    public void SetTypeBeforeReady(int type) => _savedType = type;
    
    public void PickedType(int type)
    {
        var a =GetNode<UpgradeTree>("UI/UpgradeTree");
        var b = (new[] {"res://scene/upgrade_tree/knifer.tscn", "res://scene/upgrade_tree/musketer.tscn", "res://scene/upgrade_tree/bowman.tscn", "res://scene/upgrade_tree/mage.tscn"})[type];
        a.AddToTree(GD.Load<PackedScene>(b), GameManager.Instance.Player);
    }
    public override void _Ready()
    {
        GameManager.Instance.Game = this;
        GameManager.Instance.Pausable = GetNode<Node2D>("Pausable");
        GameManager.Instance.UI = GetNode<CanvasLayer>("UI");
        GameManager.Instance.Player = GetNode<Player>("Pausable/map/Player");
        
        PickedType(_savedType);
    }
}
