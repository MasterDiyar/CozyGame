using Godot;
using System;

public partial class Movement : Node2D
{
	[Export] private CharacterBody2D _player;
	[Export] private Dash _dash; 
	[Export] private BodyStructure _visual;
    
	private Vector2 _lastDirection = Vector2.Right;

	public override void _PhysicsProcess(double delta)
	{
		if (_dash.IsDashing) {
			_player.MoveAndSlide();
			return;
		}

		Vector2 inputDir = Input.GetVector("a", "d", "w", "s");
		
		if (_player.Velocity.Length() > 10f) _visual.PlayAnimation();
		else _visual.PlayIdleAnimation();
        
		if (inputDir != Vector2.Zero) {
			_lastDirection = inputDir.Normalized();
			HandleNormalMovement(inputDir, (float)delta);
		}else 
			ApplyFriction((float)delta);
		
		if (Input.IsActionJustPressed("shift"))
			_dash.StartDash(_player, _lastDirection);

		_player.MoveAndSlide();
	}

	private void HandleNormalMovement(Vector2 dir, float delta)
	{
		float targetSpeed = (_player is Unit unit) ? unit.MaxSpeed : 300f;
		_player.Velocity = _player.Velocity.MoveToward(dir * targetSpeed, 1500f * delta);
	}

	private void ApplyFriction(float delta)
	{
		_player.Velocity = _player.Velocity.MoveToward(Vector2.Zero, 1000f * delta);
	}
}
