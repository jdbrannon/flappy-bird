using Godot;

public abstract class PackedScenePlacement<T> : IPackedScenePlacement where T : Node2D
{
  protected readonly float _screenWidth = (float) ProjectSettings.GetSetting("display/window/size/viewport_width");
  protected readonly float _screenHeight = (float) ProjectSettings.GetSetting("display/window/size/viewport_height");
  protected Node _parentNode;
  protected PackedScene _packedScene;
  protected int _zIndex = 0;

  public PackedScenePlacement(Node parentNode, string scenePath, float xOffset)
  {
    _parentNode = parentNode;
    _packedScene = ResourceLoader.Load<PackedScene>(scenePath);
    XOffset = xOffset;
  }

  public PackedScenePlacement(Node parentNode, string scenePath, float xOffset, int zIndex)
  {
    _parentNode = parentNode;
    _packedScene = ResourceLoader.Load<PackedScene>(scenePath);
    _zIndex = zIndex;
    XOffset = xOffset;
  }

  public virtual bool ShouldPlaceNextScene(Vector2 cameraPosition)
  {
    return cameraPosition.X + _screenWidth + XOffset > LastSceneX;
  }
  
  public virtual void PlaceNextScene()
  {
    var curScene = (T) _packedScene.Instantiate();
		_parentNode.AddChild(curScene);

		curScene.Position = new Vector2(NextSceneX, NextSceneY);
    curScene.ZIndex = _zIndex;

		LastSceneX = NextSceneX;
  }
  public void InitializeScenes(Vector2 birdPosition)
  {
    while(ShouldPlaceNextScene(birdPosition))
    {
      PlaceNextScene();
    }
  }

  public float XOffset { get; }
  protected float LastSceneX { get; set; }
  public float NextSceneX => LastSceneX + XOffset;
  protected abstract float NextSceneY { get; }
}