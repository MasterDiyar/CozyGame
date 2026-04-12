using Godot;
using System;

public partial class PlayerCam : Camera2D
{
	
	private float _duration = 0, _magnitude = 0, _cur = 0, _speed = 0;
	
	private bool _isShaking = false;
	public bool IsShaking => _isShaking;
	Vector2 direction = Vector2.Zero;

	public void Shake(float duration, float magnitude, float speed)
	{
		Shake(duration, magnitude, speed, Vector2.Up);
	}

	public void Shake(float duration, float magnitude, float speed, Vector2 dir)
	{
		_duration = duration;
		_magnitude = magnitude;
		_isShaking = true;
		direction = dir;
		_speed = speed;

		direction += new Vector2(
			(float)GD.RandRange(-1.0, 1.0),
			(float)GD.RandRange(-1.0, 1.0))
		             * (_magnitude * 0.2f);
	}

	public override void _Process(double delta)
	{
		if (!_isShaking) 
		{
			Offset = Vector2.Zero;
			_cur = 0;
			return;
		}

		float dt = (float)delta;
		_duration -= dt;

		if (_duration <= 0)
		{
			_isShaking = false;
			Offset = Vector2.Zero;
			return;
		}

		_cur += dt * _speed;

		_magnitude = Mathf.MoveToward(_magnitude, 0, _magnitude * dt * 5.0f); 

		Offset = direction * _magnitude * Mathf.Sin(_cur);
	}
}
