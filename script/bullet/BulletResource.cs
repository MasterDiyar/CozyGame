using Godot;

[GlobalClass] public partial class BulletResource : Resource
{
    [Export] public Texture2D Texture;
    [Export] public float TextureRotation;
    [Export] public Rect2 TextureRect;

    [Export] public float Speed;
    [Export] public float Acceleration;
    [Export] public float LifeTime;
    [Export] public float LifeTimeConsume;
    [Export] public float Damage;

    [Export] public PackedScene[] Extras;

}