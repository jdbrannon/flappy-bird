using Godot;

public class BackgroundScenePlacement : PackedScenePlacement<Sprite2D>, IPackedScenePlacement
{
  private const float BackgroundWidth = 288;
  private const float BackgroundHeight = 512;
	

  public BackgroundScenePlacement(Node parentNode)
    : base(parentNode, "../scenes/background.tscn", BackgroundWidth, -2)
  {
    LastSceneX = -BackgroundWidth;
  }
  
  protected override float NextSceneY => _screenHeight - (BackgroundHeight / 2);
}