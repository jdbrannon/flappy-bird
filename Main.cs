using System;
using Godot;

public partial class Main : Node
{
	[Export]
	public Camera2D camera;
	[Export]
	public CharacterBody2D bird;

	private const float PipeOffset = 300;
	private readonly float _screenWidth = (float) ProjectSettings.GetSetting("display/window/size/viewport_width");
	private readonly float _screenHeight = (float) ProjectSettings.GetSetting("display/window/size/viewport_height");
	private PackedScene _floorScene;
	private PackedScene _topBottomPipeScene;
	private readonly RandomNumberGenerator _rng = new RandomNumberGenerator();


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_topBottomPipeScene = ResourceLoader.Load<PackedScene>("./TopBottomPipe.tscn");
		_floorScene = ResourceLoader.Load<PackedScene>("./floor.tscn");

		InitializePipes();

		camera.Position = new Vector2(bird.Position.X, _screenHeight / 2);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		camera.Position = new Vector2(bird.Position.X, camera.Position.Y);

		if (bird.Position.X - LastPipeX + _screenWidth > PipeOffset)
		{
			CreateNextPipe();
		}

		if (bird.Position.X + _screenWidth > LastFloorX)
		{
			CreateNextFloor();
		}
	}

	private void CreateNextPipe()
	{
		var curPipe = (CanvasGroup) _topBottomPipeScene.Instantiate();
		AddChild(curPipe);

		curPipe.Position = new Vector2(NextPipeX, NextPipeY);
		LastPipeX = LastPipeX + PipeOffset;
	}

	private void CreateNextFloor()
	{
		var curFloor = (CanvasGroup) _floorScene.Instantiate();
		AddChild(curFloor);

		curFloor.Position = new Vector2(LastFloorX + _screenWidth, 0);
		LastFloorX = LastFloorX + _screenWidth;
	}

	private void InitializePipes()
	{
		LastPipeX = _screenWidth / 2 - PipeOffset;

		var pipeOnScreen = true;
		while(pipeOnScreen)
		{
			CreateNextPipe();

			if(LastPipeX + PipeOffset > _screenWidth)
				pipeOnScreen = false;
		}
	}

	private float LastPipeX { get; set; }
	private float NextPipeY => _screenHeight / 2 + (float) Math.Round((double) _rng.RandfRange(-5, 5)) * _screenHeight / 30;
	private float NextPipeX => LastPipeX + PipeOffset;
	private float LastFloorX { get; set; } = 0;
	private float NextFloorX => LastFloorX + _screenWidth;
}
