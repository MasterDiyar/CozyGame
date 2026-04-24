using Godot;

namespace Testcase.script.prefab;

public static class BulletFactory
{

    public static Poison GetPosionByLevel(int level)
    {
        var oison =GD.Load<PackedScene>("res://scene/bullets/poison.tscn").Instantiate<Poison>();
        oison.Damage = 2.13f * level + Mathf.Log(level + 1f/level) * Mathf.Pi;
        return oison;
    }
}