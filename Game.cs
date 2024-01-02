using Godot;
using System;

public partial class Game : Node2D
{

  [Export]
  private PackedScene _mobFactory = null!;

  private PathFollow2D _spawn = null!;
  private CanvasLayer _gameOver = null!;

  private readonly Random _randomizer = new();

  public override void _Ready()
  {
    _spawn = GetNode<PathFollow2D>("%PathFollow2D");
    _gameOver = GetNode<CanvasLayer>("%GameOver");
  }

  private void SpawnMob()
  {
    var mob = _mobFactory.Instantiate<Mob>();
    _spawn.ProgressRatio = (float)_randomizer.NextDouble();
    mob.GlobalPosition = _spawn.GlobalPosition;
    AddChild(mob);
  }

  private void OnTimerTimeout()
  {
    SpawnMob();
  }

  private void OnPlayerHealthDepleted()
  {
    _gameOver.Visible = true;
    GetTree().Paused = true;
  }
}
