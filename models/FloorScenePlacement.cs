using Godot;

public class FloorScenePlacement : PackedScenePlacement<Sprite2D>, IPackedScenePlacement
{
  private const float FloorWidth = 336;
  private const float FloorHeight = 112;
	

  public FloorScenePlacement(Node parentNode)
    : base(parentNode, "../scenes/floor.tscn", FloorWidth)
  {
    LastSceneX = -_screenWidth - FloorWidth;
  }
  
  protected override float NextSceneY => _screenHeight - (FloorHeight / 2);
}
