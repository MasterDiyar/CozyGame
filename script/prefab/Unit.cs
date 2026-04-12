using Godot;
using System;

public partial class Unit : CharacterBody2D
{
	[Export] public float MaxHp = 100f;
	[Export] public float MaxSpeed = 20f;
	[Export] public float MaxPatrons = 4f;
	
	public float Hp, Patrons;
	public override void _Ready()
	{
		Hp = MaxHp;
		Patrons = MaxPatrons;
	}
}
