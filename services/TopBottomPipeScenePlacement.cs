using System;
using Godot;

public class TopBottomPipeScenePlacement : PackedScenePlacement<CanvasGroup>, IPackedScenePlacement
{
  private const float PipeOffset = 300;
  private readonly RandomNumberGenerator _rng = new RandomNumberGenerator();

  public TopBottomPipeScenePlacement(Node parentNode)
    : base(parentNode, "../scenes/TopBottomPipe.tscn", PipeOffset, -1)
  {
    LastSceneX = _screenWidth;
  }

  protected override float NextSceneY => _screenHeight / 2 + (float) Math.Round((double) _rng.RandfRange(-5, 5)) * _screenHeight / 30;
}
