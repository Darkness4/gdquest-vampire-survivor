using Godot;
using System;

public partial class SmokeExplosion : Node2D
{
  private AnimationPlayer _animationPlayer = null!;
  private ColorRect _smoke = null!;

  private readonly Random _randomizer = new();

  public override void _Ready()
  {
    _smoke = GetNode<ColorRect>("%Smoke");
    _animationPlayer = GetNode<AnimationPlayer>("%AnimationPlayer");
    Init();
  }

  private float RandomFloat(double min, double max)
  {
    return (float)(_randomizer.NextDouble() * (max - min) + min);
  }

  private async void Init()
  {
    if (_smoke.Material is ShaderMaterial shaderMaterial)
    {
      shaderMaterial.SetShaderParameter(
        "texture_offset",
        new Vector2(RandomFloat(0.0, 1.0), RandomFloat(0.0, 1.0))
      );
    }
    _animationPlayer.Play("explosion");
    await ToSignal(_animationPlayer, "animation_finished");

    QueueFree();
  }
}
