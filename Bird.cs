using Godot;
using System;
using System.Runtime.InteropServices;

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
	
	public const float JumpVelocity = -350f;
	public const float Speed = 200f;
	public const float RotationSpeed = 135f;
	
	public override void _Ready()
	{
		BirdSprite.Play("Fly");
		Velocity = new Vector2(Speed, 0);
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		float rotation = Rotation;

		if (GetSlideCollisionCount() > 0 && !IsDead)
		{
			velocity = new Vector2(0, 0);
			DieAudio.Play();
			BirdSprite.Play("Idle");
			IsDead = true;
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
			SetRotationDegrees(-45);
		}

		// Calculate rotation
		if (RotationDegrees < 90)
		{
			SetRotationDegrees(RotationDegrees + RotationSpeed * (float)delta);
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	public bool IsDead { get; private set; } = false;
}
