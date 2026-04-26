using Godot;
using System;

public partial class Attack : Node2D
{
	[Export] public WeaponResource WeaponRes;
	public PackedScene Bullet, particle;
	public BulletResource BulletResource;
	public int count;
	public float AngleOffset, SpawnOffset, StartAngle, randomness;

	public float attackTime = 2;
	public CpuParticles2D particles;
	

	public float _timer = 0;
	public override void _Ready()
	{
		SetupWeapon(WeaponRes);
	}

	public void SetupWeapon(WeaponResource res)
	{
		WeaponRes = res;
		Bullet = WeaponRes.BulletScene;
		BulletResource = (BulletResource)WeaponRes.BulletResource.Duplicate();
		count = WeaponRes.Count;
		AngleOffset = WeaponRes.AngleOffset;
		StartAngle = WeaponRes.AngleStarter;
		attackTime = WeaponRes.AttackTime;
		particle = WeaponRes.ParticleScene;
		SpawnOffset = WeaponRes.SpawnOffset;
		randomness = WeaponRes.AngleRandomness;
		
		particles?.QueueFree();
		
		particles = particle.Instantiate<CpuParticles2D>();
		particles.OneShot = true;
		particles.Lifetime = attackTime;
		particles.SetEmitting(false);
		AddChild(particles);

	}

	public override void _Process(double delta)
	{
		var dt = (float)delta;
		_timer += dt;
		
	}

	public void ExecuteAttack(float angle, Unit executer)
	{
		if (_timer < attackTime) return; _timer = 0;
		var totalAngle = angle + StartAngle;
		particles.Position = Vector2.FromAngle(totalAngle + AngleOffset) * SpawnOffset;
		particles.SetEmitting(true);
		for (int i = 0; i < count; i++) {
			var arrand = (GD.Randf() - 0.5f) * randomness;
			var bullet = Bullet.Instantiate<Bullet>();
			bullet.Rotation = totalAngle  + AngleOffset * i + arrand;
			bullet.bulletResource = BulletResource;
			bullet.dontTouchUnit = executer;
			bullet.GlobalPosition = GlobalPosition + Vector2.FromAngle(totalAngle + AngleOffset * i) * SpawnOffset;
			GameManager.Instance.Pausable.AddChild(bullet);
		}
	}
}
