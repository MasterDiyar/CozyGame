using Godot;
using System;

public partial class TimelessEffect : Node
{
    private Unit _unit;
    [Export] public float Duration;
    [Export] public float DoPerTime;
    
    float currentTime=0;

    public override void _Ready()
    {
        _unit = (Unit)GetParent();
    }

    public override void _Process(double delta)
    {
        currentTime+=(float)delta;
        if (currentTime >= Duration) QueueFree();
        if (!(currentTime >= DoPerTime)) return;
        Duration -= currentTime;
        currentTime = 0;
        Execute(_unit);
    }

    public virtual void Execute(Unit unit)
    {
        
    }
}
