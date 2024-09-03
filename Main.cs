using System;
using System.Collections.Generic;
using Godot;

public partial class Main : Node
{
	[Export]
	public Camera2D camera;
	[Export]
	public CharacterBody2D bird;

	private List<IPackedScenePlacement> _scenePlacementServices = new List<IPackedScenePlacement>();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		camera.Position = new Vector2(bird.Position.X, bird.Position.Y);

		_scenePlacementServices.Add(new BackgroundScenePlacement(this));
		_scenePlacementServices.Add(new FloorScenePlacement(this));
		_scenePlacementServices.Add(new TopBottomPipeScenePlacement(this));

		foreach(var scenePlacementService in _scenePlacementServices)
		{
			scenePlacementService.InitializeScenes(camera.Position);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		camera.Position = new Vector2(bird.Position.X, camera.Position.Y);

		foreach(var scenePlacementService in _scenePlacementServices)
		{
			if (scenePlacementService.ShouldPlaceNextScene(camera.Position))
			{
				scenePlacementService.PlaceNextScene();
			}
		}
	}
}
