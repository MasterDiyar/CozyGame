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

	public void TakeDamage(float damage)
	{
		TakeRawDamage(damage);
	}

	public void TakeRawDamage(float damage)
	{
		Hp -= damage;
		if (Hp < 0) OnDie();
	}

	void OnDie()
	{
		foreach (var VARIABLE in GetChildren())
		{
			if (VARIABLE is ThrowOnDie d)
				d.ExecuteOrder66();
		}
		QueueFree();
	}
}
