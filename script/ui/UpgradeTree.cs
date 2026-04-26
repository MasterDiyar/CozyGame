using Godot;
using System;
using Testcase.script.player;

public partial class UpgradeTree : Control
{
	public override void _Input(InputEvent evt)
	{
		if (evt.IsActionPressed("esc"))
			Hide();
	}

	public void AddToTree(PackedScene node, Player player)
	{
		var a = node.Instantiate<UpgradeBranch>();
		a.SetPlayer(player);
		GetNode<Control>("ButtonList").AddChild(a);
	}
}
