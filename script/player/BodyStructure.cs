using Godot;
using System;

public partial class BodyStructure : Node2D
{
    [Export] Sprite2D _body, _head, _leftArm, _rightArm, _leftLeg, _rightLeg;
    [Export] public AnimationPlayer Anim;
    public enum Telo
    {
        Stick,
        Average,
        Huge,
        Giant
    }

    public enum BodyPart
    {
        Head,
        Torso,
        LeftLeg,
        RightLeg,
        LeftArm,
        RightArm,
    }

    [Export] public Telo MyBody = Telo.Stick;

    public Action BodyChanged;
    public Action TextureChanged;

    public void ChangePart(BodyPart part, Texture2D texture)
    {
        switch (part) {
            case BodyPart.Head: _head.Texture = texture; break;
            case BodyPart.Torso: _body.Texture = texture; break;
            case BodyPart.LeftArm: _leftArm.Texture = texture; break;
            case BodyPart.RightArm: _rightArm.Texture = texture; break;
            case BodyPart.LeftLeg: _leftLeg.Texture = texture; break;
            case BodyPart.RightLeg: _rightLeg.Texture = texture; break;
        }
        TextureChanged?.Invoke();
    }

    public void PlayAnimation()
    {
        switch (MyBody)
        {
            case Telo.Stick:
                Anim.Play("StickMove");
                break;
        }
    }

    public void PlayIdleAnimation()
    {
        switch (MyBody)
        {
            case Telo.Stick:
                Anim.Play("StickIdle1");
                break;
        }
    }
}
