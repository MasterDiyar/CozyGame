using Godot;
using System;

public partial class Bullet : Area2D
{
	private float Speed = 0;
	private float Acceleration = 0;
	private float Damage = 0;
	private float LifeTime = 0;
	public Unit dontTouchUnit;
	[Export] public BulletResource bulletResource;
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered; 
		if (bulletResource == null) return;
		Speed = bulletResource.Speed;
		Acceleration = bulletResource.Acceleration;
		Damage = bulletResource.Damage;
		LifeTime = bulletResource.LifeTime;

		var texture = GetNode<Sprite2D>("Texture");
		texture.Texture = bulletResource.Texture;
		texture.RegionEnabled = true;
		texture.RegionRect = bulletResource.TextureRect;
		texture.Rotation = bulletResource.TextureRotation;

		if (bulletResource.Extras == null || bulletResource.Extras.Length <= 0) return;
		foreach (var extra in bulletResource.Extras)
			AddChild(extra.Instantiate());

	}

	private void OnBodyEntered(Node2D body)
	{
		if (body is not Unit uit) return;
		if (uit == dontTouchUnit) return;
			
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
}
