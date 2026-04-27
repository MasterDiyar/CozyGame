using Godot;
using System;

public partial class ClockWork : Sprite2D
{
	ShaderMaterial material;
	private float lifeTime, currentTime = 0;
	public override void _Ready()
	{
		material = Material as ShaderMaterial;
		lifeTime = GetParent<Bullet>().bulletResource.LifeTime;
	}

	public override void _Process(double delta)
	{
		currentTime += (float)delta;
		float fillAmount = currentTime / lifeTime;
		fillAmount = Mathf.Clamp(fillAmount, 0.0f, 1.0f);
		material.SetShaderParameter("fill_amount", fillAmount);
	}
}
