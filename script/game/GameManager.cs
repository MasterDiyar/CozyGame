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

    public void GoToMenu()
    {
        foreach (var nodes in Pausable.GetChildren()) nodes.QueueFree();
    }
}
