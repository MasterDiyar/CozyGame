using Godot;
using System;
using Testcase.script.player;

public partial class UpgradeBranch : Control
{
	[Export] public Effect SetWeapon;
	[Export] private WeaponResource _weaponResource;
	public override void _Ready()
	{
		Player pl = (Player)GameManager.Instance.Player;
		SetWeapon.Apply(pl);
		pl.Attack.WeaponRes = _weaponResource;
	}
}
