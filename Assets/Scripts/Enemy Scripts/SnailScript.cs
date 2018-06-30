using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailScript : MonoBehaviour {

	public float moveSpeed = 1f;
	private Rigidbody2D myBody;
	private Animator anim;

	private bool moveLeft;

	public Transform down_Collision;

	void Awake(){
		myBody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

	void Start () {
		moveLeft = true;
	}
	
	void Update () {
		if(moveLeft){
			// negative velocity for moving left. Static argument in y-axis velocity
			myBody.velocity = new Vector2 (-moveSpeed, myBody.velocity.y);
		}else{
			myBody.velocity = new Vector2 (moveSpeed, myBody.velocity.y);

		}

		CheckCollision();
	}

	void CheckCollision(){
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
		}else{
			// to face the snail left, we set its transform value to negative (using Abs)
			tempScale.x = -Mathf.Abs(tempScale.x);
		}

		transform.localScale = tempScale;
	}
}
