using Godot;
using System;
using System.Collections.Generic;
using Testcase.script.prefab;

public partial class Bullet : Area2D
{
	private float Speed = 0;
	private float Acceleration = 0;
	private float Damage = 0;
	private float LifeTime = 0;
	private float LifeTimeConsume = 0;
	public Unit dontTouchUnit;
	[Export] public BulletResource bulletResource;
	
	List<TimelessEffect> effects;
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered; 
		if (bulletResource == null) return;
		Speed = bulletResource.Speed;
		Acceleration = bulletResource.Acceleration;
		Damage = bulletResource.Damage;
		LifeTime = bulletResource.LifeTime;
		LifeTimeConsume = bulletResource.LifeTimeConsume;

		var texture = GetNode<Sprite2D>("Texture");
		texture.Texture = bulletResource.Texture;
		texture.RegionEnabled = true;
		texture.RegionRect = bulletResource.TextureRect;
		texture.Rotation = bulletResource.TextureRotation;

		if (bulletResource.Extras == null || bulletResource.Extras.Length <= 0) return;
		
		GetEffects();

	}

	private void OnBodyEntered(Node2D body)
	{
		if (body is not Unit uit) return;
		if (uit == dontTouchUnit) return;
		LifeTime -= LifeTimeConsume;
		uit.TakeDamage(Damage);
	}

	public override void _Process(double delta)
	{
		float dt = (float)delta;
		Position += dt * Vector2.FromAngle(GlobalRotation) * Speed;
		Speed += dt * Acceleration;
		LifeTime -= dt;
		if (LifeTime <= 0) QueueFree();
	}

	void GetEffects()
	{
		foreach (var extra in bulletResource.Extras) switch (extra)
		{
			case 1:
				effects.Add(BulletFactory.GetPosionByLevel(1));
				break;
			case 2:
				effects.Add(BulletFactory.GetPosionByLevel(2));
				break;
			case 3:
				effects.Add(BulletFactory.GetPosionByLevel(3));
				break;
			case 4:
				//TODO add other effects and make code more elegant
				break;
		}
	}
}
