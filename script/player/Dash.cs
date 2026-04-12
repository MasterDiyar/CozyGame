using Godot;
using System;

public partial class Dash : Node2D
{
	[Signal] public delegate void DashStartedEventHandler();
 	[Signal] public delegate void DashFinishedEventHandler();
	
    [Export] public float DashSpeed = 1500f;
	[Export] public float DashDuration = 0.2f;
	[Export] public float GhostInterval = 0.05f;
	
	[Export] public Color GhostColor = new Color(0.5f, 0.8f, 1.0f, 0.6f);
	[Export] public float GhostFadeTime = 0.3f;
	[Export] private Node2D _playerBody;
	
	[Export] Timer _dashTimer, _ghostTimer;
	
	[Export] private PlayerCam _playerCam;
	
	private bool _isDashing = false;
	private Vector2 _lastDirection = Vector2.Right;
	private CharacterBody2D _target;
	
	
	public bool IsDashing => _isDashing;

	public override void _Ready()
	{
		_dashTimer.WaitTime = DashDuration;
		_dashTimer.Timeout += OnDashTimeout;
		
		_ghostTimer.WaitTime = GhostInterval;
		_ghostTimer.Timeout += CreateGhost;

	}

	public void StartDash(CharacterBody2D target, Vector2 direction)
	{
		_target = target;
		if (_isDashing || direction == Vector2.Zero) return;

		_isDashing = true;
		_target.Velocity = direction * DashSpeed;
        
		_dashTimer.Start();
		_ghostTimer.Start();
		EmitSignal(SignalName.DashStarted);
		_playerCam.Shake(DashDuration, 20, 500);
	}

	private void OnDashTimeout()
	{
		_isDashing = false;
		_ghostTimer.Stop();
		_target.Velocity = Vector2.Zero;
		EmitSignal(SignalName.DashFinished);
	}

	private void CreateGhost() 
	{
		Node2D ghostContainer = new Node2D();
		ghostContainer.GlobalPosition = _playerBody.GlobalPosition;
		ghostContainer.GlobalRotation = _playerBody.GlobalRotation;
		ghostContainer.GlobalScale = _playerBody.GlobalScale;
        
		foreach (Node child in _playerBody.GetChildren()) {
			if (child is not Sprite2D originalSprite) continue;
			Sprite2D ghostSprite = (Sprite2D)originalSprite.Duplicate();
			ghostSprite.Modulate = GhostColor;
			ghostContainer.AddChild(ghostSprite);
		}
		GetTree().Root.AddChild(ghostContainer);

		Tween tween = ghostContainer.CreateTween();
		tween.TweenProperty(ghostContainer, "modulate:a", 0.0f, GhostFadeTime);
		tween.TweenProperty(ghostContainer, "scale", ghostContainer.Scale * 2, GhostFadeTime);
		tween.TweenCallback(Callable.From(ghostContainer.QueueFree));
	}
}
