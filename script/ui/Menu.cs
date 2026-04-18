using Godot;
using System;

public partial class Menu : Control
{
	[Export] Button Play, Start;
	[Export] OptionButton Pick;
	[Export] PackedScene PlayScene;

	public override void _Ready()
	{
		Play.Pressed += PlayOnPressed;
		Start.Pressed += StartOnPressed;
	}

	private void PlayOnPressed()
	{
		GetNode<Control>("Control").Show();
	}

	void StartOnPressed()
	{
		if (Pick.Selected == -1) return;
		var a = PlayScene.Instantiate<Game>();
		a.PickedType(Pick.Selected);
		GetParent().AddChild(a);
		QueueFree();
	}
}
