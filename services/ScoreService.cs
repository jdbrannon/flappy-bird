using Godot;

public class ScoreService
{
  private const string ImgBaseUrl = "assets/sprites/";
  private float _nextScorePosition;
  private float _scorePositionOffset;
  private RichTextLabel _scoreNode;


  public ScoreService(RichTextLabel scoreNode, float firstScorePosition, float scorePositionOffset)
  {
    _scoreNode = scoreNode;
    _nextScorePosition = firstScorePosition;
    _scorePositionOffset = scorePositionOffset;
  }

  public bool ShouldIncrementScore(float curPosition)
	{
		return curPosition > _nextScorePosition;
	}

	public void IncrementScore()
	{
		Score++;
		_nextScorePosition += _scorePositionOffset;
		UpdateDisplayScore();
	}

  public void UpdateDisplayScore()
	{
		var scoreString = Score.ToString();

		var text = "[center]";
		foreach(char c in scoreString)
		{
			text += $"[img]{ImgBaseUrl}{c}.png[/img]";
		}
    
    text += "[/center]";

		_scoreNode.Text = text;
	}

  public int Score { get; set; }
}
