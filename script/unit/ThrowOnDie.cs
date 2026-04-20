using Godot;
using System;
using Testcase.script.game;

public partial class ThrowOnDie : Node2D
{
	[Export] Godot.Collections.Dictionary<ResName, float> ResourceDictionary;
	[Export] private PackedScene ResourceContainerScene;
	[Export] private float maxDistance = 5;

	public void ExecuteOrder66()
	{
		if (ResourceDictionary == null) return;
		foreach (var v in ResourceDictionary)
		{
			var a = ResourceContainerScene.Instantiate<ResourceContainer>();
			a.GlobalPosition = GlobalPosition + GD.Randf() * maxDistance * Vector2.FromAngle(GD.Randf() * float.Tau);
			a.Count = v.Value;
			a.resourceName = v.Key;
			GameManager.Instance.Pausable.AddChild(a);
		}
	}
}
