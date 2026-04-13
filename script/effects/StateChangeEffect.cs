using Godot;
using Testcase.script.player;


[GlobalClass] public partial class StateChangeEffect : Effect
{
    public enum Info
    {
        Dash,
        Scale,
        UpgradeTree
    }
    
    [Export] public Info Character;
    
    
    public override void Apply(Unit unit)
    {
        if (unit is not Player pl) return;
        switch (Character)
        {
            case Info.Dash:
                pl.GetNode<Dash>("dash").TurnOn = true;
                break;
            case Info.UpgradeTree:
                GameManager.Instance.UI.Show();
                GameManager.Instance.UI.GetNode<UpgradeTree>("UpgradeTree").Show();
                break;
        }
        
    }
}