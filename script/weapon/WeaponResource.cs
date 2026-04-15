using Godot;

[GlobalClass] public partial class WeaponResource : Resource
{
    [Export] public PackedScene BulletScene;
    [Export] public int Count = 1;
    [Export] public float AngleOffset;
    [Export] public float AngleStarter;

    [Export] public float SpawnOffset;
    [Export] public float AttackTime = 1;
    
    [Export] public BulletResource BulletResource;
    [Export] public PackedScene ParticleScene;

    [ExportGroup("forItemContainer")] [Export]
    public Texture2D WeaponIcon;
    [Export]public Rect2 IconRect;
}