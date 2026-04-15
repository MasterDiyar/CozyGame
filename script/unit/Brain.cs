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
	Timer timer = new Timer();

	[Export] private Node2D texture;
	[Export] private Attack[] weapons;
	public override void _Ready()
	{
		AddChild(timer);
		timer.Timeout += Search;
		//TODO in process
	}

	void Search()
	{
		//TODO in progress
		//create choosing between states
	}

	public override void _Process(double delta)
	{
	}
}
