using Godot;
using System;

public partial class Bird : CharacterBody2D
{
	[Export]
	public AnimatedSprite2D bird_sprite;
	
	public const float JumpVelocity = -400.0f;
	
	public override void _Ready()
	{
		bird_sprite.Play("Fly");
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
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
