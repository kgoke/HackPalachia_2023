using Godot;
using System;

public class Player : KinematicBody2D{
	Vector2 direction;
	float movementSpeed = 500;
	
	float gravity = 90;
	float maxFallSpeed = 1000;
	float minFallSpeed = 5;
	
	public override void _Ready(){
		
	}

	public override void PhysicsProcess(float delta){
		//player gravity
		direction.y += gravity;
		if(direction.y > maxFallSpeed){
			direction.y = maxFallSpeed;
		}
		
		if(isOnFloor()){
			direction.y = minFallSpeed;
		}
		
		//player movements
		direction.x = Input.GetActionStrength("move_right")-Input.GetActionStrength("move_left");
		direction.x *= movementSpeed;
		direction = MoveAndSlide(direction, Vector2.Up);
	}
}
