﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailScript : MonoBehaviour {

	public float moveSpeed = 1f;
	private Rigidbody2D myBody;
	private Animator anim;

	// will be used to check raycasts against objects on this layer (Player Layer)
	public LayerMask playerLayer;

	private bool moveLeft;

	private bool canMove;
	private bool stunned;

	// correlate to empty objects that are attached to the snail object in unity
	public Transform left_Collision,right_Collision, top_Collision, down_Collision;
	// these are needed since the snail's Scale switches (i.e. whether the snail is facing left of right)
	// in this case, the position of the left_Collision and right_Collision variables need to change also
	private Vector3 left_Collision_Position, right_Collision_Position;

	void Awake(){
		myBody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();

		left_Collision_Position = left_Collision.position;
		right_Collision_Position = right_Collision.position;
	}

	void Start () {
		moveLeft = true;
		canMove = true;
	}
	
	void Update () {

		if(canMove){
			if(moveLeft){
				// negative velocity for moving left. Static argument in y-axis velocity
				myBody.velocity = new Vector2 (-moveSpeed, myBody.velocity.y);
			}else{
				myBody.velocity = new Vector2 (moveSpeed, myBody.velocity.y);
			}
		}

		CheckCollision();
	}

	void CheckCollision(){

		// raycast to check left and right side collision against 'Player' layer objects only
		RaycastHit2D leftHit = Physics2D.Raycast(left_Collision.position, Vector2.left, 0.15f, playerLayer);
		RaycastHit2D rightHit = Physics2D.Raycast(right_Collision.position, Vector2.right, 0.15f, playerLayer);

		// uses a circle raycast as opposed to a line raycast (above). has a radius of 0.2f
		Collider2D topHit = Physics2D.OverlapCircle(top_Collision.position, 0.2f, playerLayer);

		if(topHit != null){
			if(topHit.gameObject.tag == Tags.PLAYER_TAG){
				if(!stunned){
					// bounces player in y-axis with a velocity of 7f
					topHit.gameObject.GetComponent<Rigidbody2D>().velocity =
						new Vector2 (topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 7f);
					
					canMove = false;
					// stops snail from moving
					myBody.velocity = new Vector2(0,0);

					anim.Play("Stunned");
					stunned = true;
				}
			}
		}

		if(leftHit){
			if(leftHit.collider.gameObject.tag == Tags.PLAYER_TAG){
				if(!stunned){
					// APPLY DAMAGE TO PLAYER
					Debug.Log("DO SOME DAMAGE");
				}else{
					// pushes snail object to right side with velocity 15f
					myBody.velocity = new Vector2(15f, myBody.velocity.y);
				}
			}
		}

		if(rightHit){
			if(rightHit.collider.gameObject.tag == Tags.PLAYER_TAG){
				if(!stunned){
					// APPLY DAMAGE TO PLAYER
					Debug.Log("DO SOME DAMAGE");
				}else{
					// pushes snail object to left side with velocity 15f
					myBody.velocity = new Vector2(-15f, myBody.velocity.y);
				}
			}
		}

		// if we don't detect collision, move in the opposite direction
		if(!Physics2D.Raycast(down_Collision.position, Vector2.down, 0.1f)){
			ChangeDirection();
		}
	}

	void ChangeDirection(){
		moveLeft = !moveLeft;

		// can't change transform directly, so is done through a temp variable
		Vector3 tempScale = transform.localScale;

		if(moveLeft){
			// to face the snail left, we set its transform value to positive (using Abs)
			tempScale.x = Mathf.Abs(tempScale.x);
			// by default the collision on the sides of the snail are set to the default also
			left_Collision.position = left_Collision_Position;
			right_Collision.position = right_Collision_Position;
		}else{
			// to face the snail left, we set its transform value to negative (using Abs)
			tempScale.x = -Mathf.Abs(tempScale.x);
			// swap collision positions on the sides of the snail in the instance of the snail 
			// facing the opposite direction (i.e. right side)
			left_Collision.position = right_Collision_Position;
			right_Collision.position = left_Collision_Position;
		}

		transform.localScale = tempScale;
	}

}
