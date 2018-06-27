using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float speed = 5f;

	private Rigidbody2D myBody;
	private Animator anim;

	public Transform groundCheckPosition;
	public LayerMask groundLayer;

	void Awake(){
		// gets component 'Rigidbody2D' from the current object
		myBody = GetComponent<Rigidbody2D>(); 
		// gets component 'Animator' from the current object
		anim = GetComponent<Animator>();

	}

	void Start () {
		
	}
	
	void Update () {
		if(Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.5f, groundLayer)){
			print("Collided with ground");
		}
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
			ChangeDirection(1); // scale of 1 faces player to right-side
		}else if(h<0){
			myBody.velocity = new Vector2(-speed, myBody.velocity.y);
			ChangeDirection(-1); // scale of -1 faces player to left-side
		}else{
			// velocity of 0 on x-axis if user input == 0. i.e fixes sliding
			myBody.velocity = new Vector2(0f, myBody.velocity.y);
		}

		// must match the parameter within 'Animator' tab to match 'Speed'
		// value of myBody.velocity is passed into the Animator object to project an 
		// animation based on set constraints within 'Animator' tab  
		anim.SetInteger("Speed", Mathf.Abs((int)(myBody.velocity.x)));
	}

	void ChangeDirection(int direction){
		// localScale uses z-axis even though 2D games don't need it
		Vector3 tempScale = transform.localScale;
		tempScale.x = direction;
		// store scale back with modified x-position
		transform.localScale = tempScale;

		// NOTE: a temporary variable since c-sharp doesn't allow modifying the scale
		// 		 directly 
	}
	
}
