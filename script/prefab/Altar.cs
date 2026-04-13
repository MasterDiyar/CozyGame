using Godot;
using System;
using Testcase.script.effects;
using Testcase.script.player;

public partial class Altar : Area2D
{
	[Export] Effect effect;
	private Button btn;
	private Player _pl;
	public override void _Ready()
	{
		btn = GetNode<Button>("Button");
		BodyEntered += OnAreaEntered;
		BodyExited += OnBodyExited;
		btn.Pressed += () => effect.Apply(_pl);
		btn.Hide();
	}

	protected virtual void OnBodyExited(Node2D body)
	{
		if (body is Player pl)
		{
			btn.Hide();
			_pl = pl;
		}
	}

	protected virtual void OnAreaEntered(Node2D area)
	{
		if (area is Player pl)
		{
			btn.Show();
			_pl = pl;
		}
	}
	
	
}
