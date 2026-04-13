using Godot;
using System;

public partial class UpgradeTree : Control
{
	public override void _Input(InputEvent evt)
	{
		if (evt.IsActionPressed("esc"))
			Hide();
	}
}
