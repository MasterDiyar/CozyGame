using Godot;
using System;

public partial class Attack : Node2D
{
	[Export] PackedScene Bullet;
	[Export] BulletResource BulletResource;
	[Export] private int count;
	[Export] private float AngleOffset, SpawnOffset;

	[Export] private float attackTime = 2;
	

	private float _timer = 0;

	public override void _Process(double delta)
	{
		var dt = (float)delta;
		_timer += dt;
		
	}

	public void ExecuteAttack(float angle, Unit executer)
	{
		if (_timer < attackTime) return;
		_timer = 0;
		for (int i = 0; i < count; i++) {
			var bullet = Bullet.Instantiate<Bullet>();
			bullet.Rotation = angle + AngleOffset * i;
			bullet.bulletResource = BulletResource;
			bullet.dontTouchUnit = executer;
			bullet.Position = GlobalPosition + Vector2.FromAngle(angle + AngleOffset * i) * SpawnOffset;
			GameManager.Instance.Pausable.AddChild(bullet);
		}
	}
}
