using Godot;

public partial class Gun : Area2D
{
  private Marker2D _shootingPoint = null!;

  [Export]
  private PackedScene _bulletFactory = null!;

  public override void _Ready()
  {
    _shootingPoint = GetNode<Marker2D>("%ShootingPoint");
  }

  public override void _PhysicsProcess(double delta)
  {
    var bodies = GetOverlappingBodies();
    if (bodies.Count > 0)
    {
      var target = bodies[0];
      LookAt(target.GlobalPosition);
    }
  }

  private void _Shoot()
  {
    var bullet = _bulletFactory.Instantiate<Bullet>();
    bullet.GlobalPosition = _shootingPoint.GlobalPosition;
    bullet.GlobalRotation = _shootingPoint.GlobalRotation;
    _shootingPoint.AddChild(bullet);
  }

  private void OnTimerTimeout()
  {
    _Shoot();
  }
}
