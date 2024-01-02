using Godot;
using System;

public partial class HappyBoo : Node2D
{
  private AnimationPlayer _animationPlayer = null!;

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    _animationPlayer = GetNode<AnimationPlayer>("%AnimationPlayer");
  }

  public void PlayIdleAnimation()
  {
    _animationPlayer.Play("idle");
  }

  public void PlayWalkAnimation()
  {
    _animationPlayer.Play("walk");
  }
}
