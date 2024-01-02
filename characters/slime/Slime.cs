using Godot;

public partial class Slime : Node2D
{
  private AnimationPlayer _animationPlayer = null!;

  public override void _Ready()
  {
    _animationPlayer = GetNode<AnimationPlayer>("%AnimationPlayer");
  }

  public void PlayWalkAnimation()
  {
    _animationPlayer.Play("walk");
  }

  public void PlayHurtAnimation()
  {
    _animationPlayer.Play("hurt");
    _animationPlayer.Queue("walk");
  }
}
