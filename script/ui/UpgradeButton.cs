using Godot;
using System;
using Testcase.script.effects;
using Testcase.script.player;

public partial class UpgradeButton : Button
{
	[Export] public bool IsOpen = false;
	[Export] public UpgradeButton button;
	[Export] CostResource cost;
	[Export] Effect effect;
	[Export] private Line2D line;
		

	private Player _player;

	public override void _Ready()
	{
		_player = GetTree().GetFirstNodeInGroup("Player") as Player;
		Pressed += OnClick;
		
		Disabled = IsOpen;
		
	}
	
	bool CheckRequirements()
	{
		if (button.IsOpen)
		{
			Show();
			
			
			
			
			
		}

		return false;
	}
	
	void OnClick()
	{
		if (cost.CanAfford(_player.CurrentResources)) {
			cost.Spend(_player.CurrentResources); 
			effect.Apply(_player);               
			IsOpen = true;
		}
	}
}
