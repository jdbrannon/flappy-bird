using System.Collections.Generic;
using Godot;

public partial class Main : Node
{
	[Export]
	public Camera2D Camera;
	[Export]
	public CharacterBody2D Bird;
	[Export]
	public RichTextLabel ScoreNode;

	private List<IPackedScenePlacement> _scenePlacementServices = new List<IPackedScenePlacement>();
	private ScoreService _scoreService;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var topBottomPipeScenePlacement = new TopBottomPipeScenePlacement(this);

		_scoreService = new ScoreService(ScoreNode, topBottomPipeScenePlacement.NextSceneX, topBottomPipeScenePlacement.XOffset);

		_scenePlacementServices.Add(new BackgroundScenePlacement(this));
		_scenePlacementServices.Add(new FloorScenePlacement(this));
		_scenePlacementServices.Add(topBottomPipeScenePlacement);

		foreach(var scenePlacementService in _scenePlacementServices)
		{
			scenePlacementService.InitializeScenes(Camera.Position);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Camera.Position = new Vector2(Bird.Position.X, Camera.Position.Y);

		foreach(var scenePlacementService in _scenePlacementServices)
		{
			if (scenePlacementService.ShouldPlaceNextScene(Camera.Position))
			{
				scenePlacementService.PlaceNextScene();
			}
		}

		if (_scoreService.ShouldIncrementScore(Bird.Position.X))
		{
			_scoreService.IncrementScore();
		}
	}
}
