using Godot;

namespace Testcase.script.effects;

[GlobalClass]public partial class CostResource : Resource
{
    [ExportGroup("level")]
    [Export] public float Strength;
    [Export] public float Density;
    [Export] public float Versatility;

    [ExportGroup("resources")] 
    [Export] public float Silicon;
    [Export] public float Iron;
    [Export] public float Gold;
    [Export] public float Silver;
    [Export] public float Bronze;
    
    public bool CanAfford(CostResource wallet)
    {
        if (wallet.Strength < Strength) return false;
        if (wallet.Density < Density) return false;
        if (wallet.Versatility < Versatility) return false;

        if (wallet.Silicon < Silicon) return false;
        if (wallet.Iron < Iron) return false;
        if (wallet.Gold < Gold) return false;
        if (wallet.Silver < Silver) return false;
        if (wallet.Bronze < Bronze) return false;

        return true;
    }

    public void Spend(CostResource wallet)
    {
        wallet.Strength -= Strength;
        wallet.Density -= Density;
        wallet.Versatility -= Versatility;
        wallet.Silicon -= Silicon;
        wallet.Iron -= Iron;
        wallet.Gold -= Gold;
        wallet.Silver -= Silver;
        wallet.Bronze -= Bronze;
    }
}