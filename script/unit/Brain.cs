using Godot;
using System;
using Testcase.script.player;
using Testcase.script.unit;

public partial class Brain : Node2D
{
	public enum State
	{
		Move,
		Idle,
		Prepare,
		March,
		Shoot
	}
	Timer timer = new();

	[Export] private Node2D texture;
	[Export] private Attack[] weapons;
	[Export] public float Distance = 100;
	[Export] NodePath AnimationPlayerPath;
	
	State _currentState = State.Idle, _preferableState = State.Idle;

	Player _player;
	Unit _unit;
	Pathways _pathways;
	AnimationPlayer _animationPlayer;
	bool _hasPathways = false;
	public override void _Ready()
	{
		_animationPlayer = GetNode<AnimationPlayer>(AnimationPlayerPath);
		_unit = GetParent<Unit>();
		_player = (Player)GameManager.Instance.Player;
		AddChild(timer);
		timer.Timeout += Search;
		foreach (var child in _unit.GetChildren()) {
			if (child is not Pathways pw) continue;
			_pathways = pw;
			_hasPathways = true;
		}
		timer.Start();
	}

	void Search()
	{
		var dist = _player.GlobalPosition - GlobalPosition;
		if (dist.Length() >= Distance/2)
			_preferableState = State.Prepare;
		else if (dist.Length() >= Distance)
			_preferableState = State.Move;
		else 
			_preferableState = State.Idle;
		
		var a = GD.Randf() > 0.5f;
		_currentState = _preferableState switch {
			State.Move => a ? State.Idle : State.Move,
			State.Idle => a ? State.Move : State.Idle,
			State.Prepare => a ? State.Prepare : State.Move,
			_ => _currentState
		};
		if (_currentState == State.Prepare) timer.Stop();
	}

	private float beforeShoot = 0;

	public override void _Process(double delta)
	{
		if (_player == null) _player = (Player)GameManager.Instance.Player;
		var dt = (float)delta;
		var dir = _player.GlobalPosition - GlobalPosition;
		switch (_currentState)
		{
			case State.Move:
				_animationPlayer.Play("Move");
				dir = dir.Normalized();
				_unit.Velocity = dir * _unit.MaxSpeed * dt;
				_unit.MoveAndSlide();
				break;
			case State.Idle:
				_animationPlayer.Play("Idle");
				return;
			case State.Shoot:
				var angle = dir.Angle();
				foreach (var wep in weapons)
					wep.ExecuteAttack(angle, _unit);
				_currentState = State.Idle;
				return;
			case State.Prepare:
				_animationPlayer.Play("Prepare");
				beforeShoot += dt;
				if (!(beforeShoot > _animationPlayer.CurrentAnimationLength)) return;
				timer.Start();
				_currentState = State.Shoot;
				beforeShoot = 0;
				return;
			case State.March:
				_unit.Velocity = Vector2.FromAngle(_pathways.GetAngleToTarget(_unit.GlobalPosition)) * _unit.MaxSpeed;
				if ((_unit.Position -  _pathways.GetTargetPosition()).Length() < 5f)
					_pathways.AddIndex();
				_animationPlayer.Play("Move");
				return;
				
		}
	}
}
