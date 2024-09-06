using Godot;

public partial class Bird : CharacterBody2D
{
	[Export]
	public AnimatedSprite2D BirdSprite;
	[Export]
	public AudioStreamPlayer FlapSound;
	[Export]
	public AudioStreamPlayer DieAudio;
	[Export]
	public Sprite2D GameOverNode;
	[Export]
	public RichTextLabel ScoreNode;
	[Export]
	public Sprite2D GetReadyNode;
	
	public const float JumpVelocity = -500f;
	public const float Speed = 200f;
	public const float RotationSpeed = 270f;
	public const int PipeCollisionLayer = 4;
	
	public override void _Ready()
	{
		BirdSprite.Play("Fly");
	}

	public override void _PhysicsProcess(double delta)
	{
		if(!Started)
		{
			if(Input.IsActionJustPressed("flap"))
			{
				Velocity = new Vector2(Speed, 0);
				Visible = true;
				Started = true;
				GetReadyNode.Visible = false;
			}
			else
			{
				return;
			}
		}

		Vector2 velocity = Velocity;

		if (JustDied)
		{
			velocity = new Vector2(0, Velocity.Y);
			DieAudio.Play();
			BirdSprite.Play("Idle");
			IsDead = true;

			// Disable pipe collision so that bird can freely fall to the ground
			CollisionMask = CollisionMask - PipeCollisionLayer;
		}

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}
		else
		{
			GameOverNode.Visible = true;
			ScoreNode.Visible = false;
		}

		// Handle Jump.
		if (!IsDead && Input.IsActionJustPressed("flap"))
		{
			velocity.Y = JumpVelocity;
			FlapSound.Play();
			SetRotationDegrees(-30);
		}

		// Calculate rotation
		if (RotationDegrees < 90 && (Velocity.Y >= 100f || IsOnFloor()))
		{
			SetRotationDegrees(RotationDegrees + RotationSpeed * (float)delta);
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	public bool Started { get; private set; } = false;
	public bool IsDead { get; private set; } = false;
	private bool JustDied => !IsDead && GetSlideCollisionCount() > 0;
}
