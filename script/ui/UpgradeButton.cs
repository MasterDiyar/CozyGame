using Godot;
using System;
using Testcase.script.effects;
using Testcase.script.player;

public partial class UpgradeButton : Button
{
	[Export] public bool IsOpen = false, IsBuyed = false;
	[Export] public UpgradeButton button;
	[Export] CostResource cost;
	[Export] Effect effect;
	[Export] private Line2D line;

	public Action parentBuyed;
		

	private Player _player;

	public override void _Ready()
	{
		_player = GetTree().GetFirstNodeInGroup("player") as Player;
		Pressed += OnClick;
		MouseEntered += Tesc;
		
		Disabled = !IsOpen;
		
		if (button == null) return;
		button.parentBuyed += Tesc;
		Visible = button.IsOpen;
		CheckRequirements();
		SetLine();
	}
	

	void SetLine()
	{
		Vector2 startPos = Size / 2;
        Vector2 endPos = (button.Position + button.Size / 2) - Position;
        line.Points = [startPos, endPos];
        line.ShowBehindParent = true;
	}

	void Tesc()
	{
		CheckRequirements();
	}
	
	bool CheckRequirements()
	{
		if (button is { IsOpen: false }) return false;
		Show();
		Disabled = !cost.CanAfford(_player.CurrentResources);
		return !Disabled;
	}
	
	void OnClick()
	{
		if (cost.CanAfford(_player.CurrentResources)) {
			cost.Spend(_player.CurrentResources); 
			//effect.Apply(_player);               
			IsOpen = true;
			IsBuyed = true;
			parentBuyed?.Invoke();
		}
	}
}
