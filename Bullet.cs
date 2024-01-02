using Godot;

public partial class Bullet : Area2D
{
  private const int SPEED = 1000;
  private const int RANGE = 1200;
  private double travelledDistance = 0;

  public override void _PhysicsProcess(double delta)
  {
    var direction = Vector2.Right.Rotated(Rotation);
    Position += direction * (float)(SPEED * delta);

    travelledDistance += SPEED * delta;
    if (travelledDistance > RANGE)
    {
      QueueFree();
    }
  }

  private void OnBodyEntered(Node2D body)
  {
    if (body is IDamageable damageable)
    {
      damageable.TakeDamage();
    }
    QueueFree();
  }
}
