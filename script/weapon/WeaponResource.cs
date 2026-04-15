using Godot;

[GlobalClass] public partial class WeaponResource : Resource
{
    [Export] public PackedScene BulletScene;
    [Export] public int Count = 1;
    [Export] public float AngleOffset = 0;
    [Export] public float AngleStarter = 0;

    [Export] public float SpawnOffset = 0;
    [Export] public float AttackTime = 1;
    
    [Export] public BulletResource BulletResource;
    [Export] public PackedScene ParticleScene;
}