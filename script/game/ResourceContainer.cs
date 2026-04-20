using Godot;
using Testcase.script.player;

namespace Testcase.script.game;

//TODO this is slop, WIP
public partial class ResourceContainer : Area2D
{
    [Export] public ResName resourceName;
    [Export] Texture2D texture;
    [Export] public float Count = 1;
    Sprite2D sprite;
    private bool _playerIsHere;
    Player _player;
    private AudioStreamPlayer2D _audio;

    public override void _Ready()
    {
        _audio = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
        sprite = GetNode<Sprite2D>("Sprite2D");
        sprite.RegionRect = Atlas(resourceName);
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

    public override void _Input(InputEvent vent)
    {
        bool a = vent.IsActionPressed("e") && _playerIsHere;
        if (!a) return;
        _player.CurrentResources.AddResource(resourceName, Count);
        _audio.Play();
        QueueFree();
    }

    Rect2 Atlas(ResName name) => name switch {
        ResName.Bronze => new Rect2(0, 16, 16, 16),
        ResName.Iron => new Rect2(0, 0, 16, 16),
        ResName.Silicon => new Rect2(16, 0, 16, 16),
        ResName.Gold => new Rect2(32, 0, 16, 16),
        ResName.Silver => new Rect2(48, 0, 16, 16),
        _ => new Rect2(0, 0, 48, 16),
    };
}