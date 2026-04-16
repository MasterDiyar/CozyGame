using Godot;

namespace Testcase.script.unit;

public partial class Pathways : Node2D
{
    [Export] public Vector2[] Points;
    [Export] public bool repeatable = true;
    
    public int CurrentPoint = 0;
    
    public bool HasPoints => Points != null && Points.Length > 0;

    public Vector2 GetTargetPosition()
    {
        return !HasPoints ? GlobalPosition : ToGlobal(Points[CurrentPoint]);
    }

    public float GetAngleToTarget(Vector2 fromPos)
    {
        if (!HasPoints) return 0f;
        return (GetTargetPosition() - fromPos).Angle();
    }

    public void AddIndex()
    {
        if (!HasPoints) return;
        
        if (repeatable)
            CurrentPoint = (CurrentPoint + Points.Length) % Points.Length;
        else {
            if (CurrentPoint < Points.Length - 1)
                CurrentPoint++;
        }
    }
}