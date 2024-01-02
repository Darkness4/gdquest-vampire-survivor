using Godot;

public partial class Mob : CharacterBody2D, IDamageable
{
  [Export]
  private PackedScene _smokeFactory = null!;

  private Slime _slime = null!;
  private Player _player = null!;

  private const int SPEED = 200;
  private const float ACCELERATION = 0.5f;

  private int _health = 3;

  public override void _Ready()
  {
    _player = GetNode<Player>("/root/Game/Player");
    _slime = GetNode<Slime>("%Slime");
    _slime.PlayWalkAnimation();
  }

  public override void _PhysicsProcess(double delta)
  {
    var direction = GlobalPosition.DirectionTo(_player.GlobalPosition);
    var inputVelocity = direction * SPEED;
    if (inputVelocity.Length() > 0)
    {
      Velocity = Velocity.Lerp(inputVelocity, ACCELERATION);
    }
    MoveAndSlide();
  }

  public void TakeDamage()
  {
    _slime.PlayHurtAnimation();
    _health--;
    if (_health <= 0)
    {
      QueueFree();
      var smoke = _smokeFactory.Instantiate<SmokeExplosion>();
      GetParent().AddChild(smoke);
      smoke.GlobalPosition = GlobalPosition;
    }
  }
}
