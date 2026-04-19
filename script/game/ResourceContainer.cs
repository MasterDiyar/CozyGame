using Godot;
using Testcase.script.player;

namespace Testcase.script.game;

//TODO this is slop, WIP
public partial class ResourceContainer: Area2D
{
    [Export] WeaponResource weaponResource;
    Sprite2D sprite;
    private bool _playerIsHere;
    Player _player;
    private AudioStreamPlayer2D _audio;
	
    public override void _Ready()
    {
        _audio = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
        sprite = GetNode<Sprite2D>("Sprite2D");
        sprite.Texture = weaponResource.WeaponIcon;
        sprite.RegionEnabled = true;
        sprite.RegionRect = weaponResource.IconRect;
        BodyEntered += OnBodyEntered;
        BodyExited += OnBodyExited;
    }

    private void OnBodyEntered(Node2D body)
    {
        if (body is not Player player) return;
        _player = player;
        _playerIsHere = true;
    }

    void OnBodyExited(Node2D body)
    {
        _playerIsHere = false;
    }

    public override void _Input(InputEvent @event)
    {
        bool a = @event.IsActionPressed("e") && _playerIsHere;
        if (!a) return;
        _player.Attack.WeaponRes = weaponResource;
        _audio.Play();
        QueueFree();		
    }
}