using Godot;
using System;
using System.Linq;
using Testcase.script.effects;
using Testcase.script.player;

public partial class UpgradeButton : Button
{
	[Export] public bool IsOpen = false, IsBuyed = false;
	[Export] public UpgradeButton[] button;
	[Export] CostResource cost;
	[Export] Effect[] effects;
	[Export] private Line2D line;

	[Export] private UpgradeButton orButton;

	public Action parentBuyed;
		

	private Player _player;

	public override void _Ready()
	{
		_player = GetTree().GetFirstNodeInGroup("player") as Player;
		Pressed += OnClick;
		MouseEntered += Tesc;
		
		Disabled = !IsOpen;
		
		if (orButton != null)
			orButton.parentBuyed += Tesc;
		
		
		if (button == null || button.Length <= 0) return;
		foreach (var upgradeButton in button) {
			upgradeButton.parentBuyed += Tesc;
			Visible = upgradeButton.IsOpen;
		}

		TooltipText = cost.ToString();
		CheckRequirements();
		SetLine();
	}
	

	void SetLine()
	{
		for (int i = 0; i < button.Length; i++) {
			var targetBtn = button[i];
			if (targetBtn == null) continue;

			Line2D currentLine = (i == 0) ? line : (Line2D)line.Duplicate();
			if (i > 0) AddChild(currentLine);

			Vector2 startPos = Size / 2;
			Vector2 endPos = (targetBtn.Position + targetBtn.Size / 2) - Position;
			currentLine.Points = [startPos, endPos];
			currentLine.ShowBehindParent = true;
		}
	}

	void Tesc()
	{
		CheckRequirements();
	}
	
	bool CheckRequirements()
	{
		if (orButton is { IsBuyed: true }) {
			Disabled = true;
			return false;
		}

		if (button is { Length: > 0 }) {
			if (button.Any(btn => btn is { IsOpen: false })) {
				Disabled = true;
				return false; 
			}
		}
		Show();
		if (cost != null)
			Disabled = !cost.CanAfford(_player.CurrentResources);
		else Disabled = false;
		return !Disabled;
	}
	
	void OnClick()
	{
		if (IsBuyed) return;
		if (cost != null) {
			if (!cost.CanAfford(_player.CurrentResources)) return;
			cost.Spend(_player.CurrentResources);
		}
		
		if (effects == null) {
			GD.PrintErr("you forgot to add upgrade effects to :", Name);
			return;
		}
		foreach (var effect in effects)
			effect.Apply(_player);      
		
		         
		IsOpen = true;
		IsBuyed = true;
		parentBuyed?.Invoke();
		Disabled = true;
	}
}
