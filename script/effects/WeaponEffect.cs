using System;
using Godot;
using Testcase.script.player;

[GlobalClass] public partial class WeaponEffect : Effect
{
    [Export] private WhatChange _change;
    [Export] private HowChange _howChange;
    [Export] float number;
    
    public override void Apply(Unit unit)
    {
        if (unit is not Player pl) return;

        var weapon = pl.GetNode<Attack>("attack");
        
        
        
        throw new NotImplementedException();
    }

    private void ChangeWeaponStats(Attack wep, WhatChange change)
    {
        switch (change) {
            case  WhatChange.TextureRot:
                wep.BulletResource.TextureRotation = DoThing(wep.BulletResource.TextureRotation, number, _howChange);
                break;
            case WhatChange.Speed:
                wep.BulletResource.Speed = DoThing(wep.BulletResource.Speed, number, _howChange);
                break;
            case WhatChange.Acceleration:
                wep.BulletResource.Acceleration = DoThing(wep.BulletResource.Acceleration, number, _howChange);
                break;
            case WhatChange.LifeTime:
                wep.BulletResource.LifeTime = DoThing(wep.BulletResource.LifeTime, number, _howChange);
                break;
            case WhatChange.Damage:
                wep.BulletResource.Damage = DoThing(wep.BulletResource.Damage, number, _howChange);
                break;
            case WhatChange.Count:
                wep.count = (int)DoThing(wep.count, number, _howChange);
                break;
            case WhatChange.AngleOffset:
                wep.AngleOffset = DoThing(wep.AngleOffset, number, _howChange);
                break;
            case WhatChange.AngleStarter:
                wep.StartAngle = DoThing(wep.StartAngle, number, _howChange);
                break;
            case WhatChange.SpawnOffset:
                wep.SpawnOffset = DoThing(wep.SpawnOffset, number, _howChange);
                break;
            case WhatChange.AttackTime:
                wep.attackTime = DoThing(wep.attackTime, number, _howChange);
                break;
            case WhatChange.AngleRandomness:
                wep.randomness = DoThing(wep.randomness, number, _howChange);
                break;
        }
    }

    float DoThing(float from, float with, HowChange action) => action switch
    {
        HowChange.Add => from + with,
        HowChange.Reduce => from - with,
        HowChange.Equal => with,
        HowChange.Multiply => from * with,
        HowChange.Divide => from / with,
        _ => from
    };

    public enum WhatChange
    {
        TextureRot,
        Speed,
        Acceleration,
        LifeTime,
        Damage,
        Count,
        AngleOffset,
        AngleStarter,
        SpawnOffset,
        AttackTime,
        AngleRandomness
    }

    public enum HowChange
    {
        Add,
        Reduce,
        Equal,
        Multiply,
        Divide
    }
}