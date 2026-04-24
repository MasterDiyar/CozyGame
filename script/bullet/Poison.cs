using Godot;
using System;

public partial class Poison : TimelessEffect
{
    public float Damage;

    public override void Execute(Unit unit)
    {
        unit.TakeDamage(Damage);
    }
}
