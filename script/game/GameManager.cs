using Godot;
using System;

public partial class GameManager : Node
{
    public static GameManager Instance;

    public Node2D Game, Pausable, Player;

    public CanvasLayer UI;

    public override void _EnterTree()
    {
        Instance = this;
    }
}
