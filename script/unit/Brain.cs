using Godot;
using System;

public partial class Brain : Node2D
{
	public enum State
	{
		Move,
		Idle,
		Prepare,
		Shoot
	}

	[Export] private Node2D texture;
	[Export] private Node2D[] weapons;
	public override void _Ready()
	{
		//TODO in process
	}

	public override void _Process(double delta)
	{
	}
}
