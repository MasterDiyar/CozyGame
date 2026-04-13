using Godot;

[GlobalClass] public abstract partial class Effect : Resource
{
    public abstract void Apply(Unit unit);
}