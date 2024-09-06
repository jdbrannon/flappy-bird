using Godot;

public interface IPackedScenePlacement
{
  public bool ShouldPlaceNextScene(Vector2 reference);
  public void PlaceNextScene();
  public void InitializeScenes(Vector2 birdPosition);
}