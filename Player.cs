using Godot;
using System;

public partial class Player : CharacterBody2D
{
  private HappyBoo _happyBoo = null!;
  private Area2D _hitBox = null!;
  private ProgressBar _healthBar = null!;

  private const int SPEED = 600;
  private const float FRICTION = 0.3f;
  private const float ACCELERATION = 0.5f;
  private const double DAMAGE_RATE = 50.0f;
  [Signal]
  public delegate void HealthDepletedEventHandler();

  private double _health = 100.0;

  public override void _Ready()
  {
    _happyBoo = GetNode<HappyBoo>("%HappyBoo");
    _hitBox = GetNode<Area2D>("%HitBox");
    _healthBar = GetNode<ProgressBar>("%HealthBar");
    _healthBar.Value = _health;
    _healthBar.MaxValue = _health;
  }

  public override void _PhysicsProcess(double delta)
  {
    Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
    var inputVelocity = direction * SPEED;
    if (inputVelocity.Length() > 0)
    {
      Velocity = Velocity.Lerp(inputVelocity, ACCELERATION);
      _happyBoo.PlayWalkAnimation();
    }
    else
    {
      Velocity = Velocity.Lerp(Vector2.Zero, FRICTION);
      _happyBoo.PlayIdleAnimation();
    }
    MoveAndSlide();

    var bodies = _hitBox.GetOverlappingBodies();
    if (bodies.Count > 0)
    {
      _health -= DAMAGE_RATE * bodies.Count * delta;
      _healthBar.Value = _health;
      if (_health <= 0.0)
      {
        EmitSignal(nameof(HealthDepleted));
      }
    }
  }
}
