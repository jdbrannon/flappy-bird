using Godot;
using System.Collections.Generic;

public partial class Main : Node
{
	private int _speed = 400;
	private bool _flying = true;
	private PackedScene _topBottomPipeScene;
	private Node2D _lastPipe;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_topBottomPipeScene = ResourceLoader.Load<PackedScene>("./TopBottomPipe.tscn");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (ShouldCreateNewPipe()) {
			var curPipe = _topBottomPipeScene.Instantiate();
			AddChild(curPipe);

			curPipe._Set("linear_velocity", _speed);
		}
	}

	private bool ShouldCreateNewPipe() {
		if(_flying )
			return true;
		
		return false;
	}
}
