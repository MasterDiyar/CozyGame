using Godot;
using System;
using Testcase.script.player;

public partial class Game : Node2D
{
    public override void _Ready()
    {
        GameManager.Instance.Game = this;
        GameManager.Instance.Pausable = GetNode<Node2D>("Pausable");
        GameManager.Instance.UI = GetNode<CanvasLayer>("UI");
        GameManager.Instance.Player = GetNode<Player>("Pausable/map/Player");
    }
}
