using Godot;
using System;

public class Player : KinematicBody2D{
	Vector2 direction;
	float movementSpeed = 500;
	
	float gravity = 90;
	float maxFallSpeed = 1000;
	float minFallSpeed = 5;
	
	float jumpForce = 1000;
	
	AnimatedSprite animatedSprite;
	
	
	public override void _Ready(){
		animatedSprite = (AnimatedSprite)GetNode("Player/AnimatedSprite");
	
	}

	public override void _PhysicsProcess(float delta){
		// player gravity
		direction.y += gravity;
		if(direction.y > maxFallSpeed){
			direction.y = maxFallSpeed;
		}
		
		if(IsOnFloor()){
			direction.y = minFallSpeed;
		}
		
		// player movements
		direction.x = Input.GetActionStrength("move_right")-Input.GetActionStrength("move_left");
		direction.x *= movementSpeed;
		
		// jump
		if(IsOnFloor() && Input.IsActionJustPressed("jump")){
			direction.y =- jumpForce;
		}
		
		// flip sprite
		if(direction.x > 0){
			animatedSprite.FlipH = false;
		} else if(direction.x < 0){
			animatedSprite.FlipH = true;
		}
		
		// play animatio 
		
		
		direction = MoveAndSlide(direction, Vector2.Up);
	}
}
