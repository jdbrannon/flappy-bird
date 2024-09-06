using Godot;
using System;

public partial class Bird : CharacterBody2D
{
	[Export]
	public AnimatedSprite2D BirdSprite;
	[Export]
	public AudioStreamPlayer FlapSound;

	
	public const float JumpVelocity = -350f;
	public const float Speed = 200f;
	
	public override void _Ready()
	{
		BirdSprite.Play("Fly");
		Velocity = new Vector2(Speed, 0);
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("flap"))
		{
			velocity.Y = JumpVelocity;
			FlapSound.Play();
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
