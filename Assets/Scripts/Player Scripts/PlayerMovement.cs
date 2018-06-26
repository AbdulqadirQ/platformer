using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float speed = 5f;

	private Rigidbody2D myBody;
	private Animator anim;

	void Awake(){
		// gets component 'Rigidbody2D' from the current object
		myBody = GetComponent<Rigidbody2D>(); 
		// gets component 'Animator' from the current object
		anim = GetComponent<Animator>();

	}

	void Start () {
		
	}
	
	void Update () {
		
	}

	// called in fixed intervals, defined by TimeManager -> Fixed Timestep
	void FixedUpdate(){
		PlayerWalk();
	}

	void PlayerWalk(){
		// GetAxisRaw - returns integers based on input; -1 for left, 1 for right
		float h = Input.GetAxisRaw("Horizontal");
		if(h>0){
			// move 'myBody' right, keep y-axis velocity static
			myBody.velocity = new Vector2(speed, myBody.velocity.y);
		}else if(h<0){
			myBody.velocity = new Vector2(-speed, myBody.velocity.y);
		}else{
			// velocity of 0 on x-axis if user input == 0. i.e fixes sliding
			myBody.velocity = new Vector2(0f, myBody.velocity.y);
		}
	}
}
