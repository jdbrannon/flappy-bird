using Godot;
using System;

public partial class Bird : CharacterBody2D
{
	[Export]
	public AnimatedSprite2D bird_sprite;
	
	public const float JumpVelocity = -500.0f;
	public const float Speed = 250f;
	
	public override void _Ready()
	{
		bird_sprite.Play("Fly");
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
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
