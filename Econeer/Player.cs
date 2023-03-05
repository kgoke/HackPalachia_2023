using Godot;
using System;

public class Player : KinematicBody2D{
	Vector2 direction;
	float movementSpeed = 200;
	
	float gravity = 30;
	float maxFallSpeed = 1000;
	float minFallSpeed = 5;
	
	float jumpForce = 400;
	
	AnimatedSprite animatedSprite;
	
	
	public override void _Ready(){
		animatedSprite = (AnimatedSprite)GetNode("AnimatedSprite");
	
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
		
		// play animation
		if(IsOnFloor() && direction.x == 0){
			// chop
			if(Input.IsActionPressed("swing_axe")){
				
				animatedSprite.Play("MC_Chop"); // Chop
				
			} else {
				animatedSprite.Play("MC_Idle"); // idle
			}
		} else if(IsOnFloor() && direction.x != 0){
			if(Input.IsActionJustPressed("swing_axe")){
				animatedSprite.Play("MC_Chop"); // Chop
			} else {
				animatedSprite.Play("MC_Walk");	// run 
			}
		} else if(!IsOnFloor()){
			animatedSprite.Play("MC_Jump");	// jump
		}
		
		
		direction = MoveAndSlide(direction, Vector2.Up);
	}
}
