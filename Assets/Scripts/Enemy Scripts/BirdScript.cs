using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour {

	private Rigidbody2D myBody;
	private Animator anim;

	private Vector3 moveDirection = Vector3.left;
	private Vector3 originPostion;
	private Vector3 movePosition;

	public GameObject birdEgg;
	public LayerMask playerLayer;
	private bool attacked;

	private bool canMove;

	private float speed = 2.5f;

	void Awake(){
		myBody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

	void Start () {
		// bird will keep moving 12 units (6 units either side of origin position)
		originPostion = transform.position;
		originPostion.x += 6f;

		movePosition = transform.position;
		movePosition.x -= 6f;

		canMove = true;
	}
	
	void Update () {
		MoveTheBird();
		DropTheEgg();
	}

	void MoveTheBird(){
		if(canMove){
			// smoothDeltaTime = 'smoothed' time between frames
			// Translate() -> moves the bird by <value> amount in a Left direction
			// (since moveDirection = Vector3.left)
			transform.Translate(moveDirection * speed * Time.smoothDeltaTime);

			// if bird's position is to the right of the origin position, change 
			// moveDirection to 'left'
			if(transform.position.x >= originPostion.x){
				moveDirection = Vector3.left;
				ChangeDirection(0.5f);
			// if bird's position is to the left of the origin position, change 
			// moveDirection to 'right'
			}else if(transform.position.x <= movePosition.x){
				moveDirection = Vector3.right;
				ChangeDirection(-0.5f);
			}

		}
	}

	void ChangeDirection(float direction){
		Vector3 tempScale = transform.localScale;
		tempScale.x = direction;
		transform.localScale = tempScale;
	}

	// drop egg if raycast touches player
	void DropTheEgg(){
		if(!attacked){
			// from player's position (transform.position), downwards (Vector2.down)
			// at an infinite line (Mathf.Infinity), checking for player (playerLayer)
			if(Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, playerLayer)){
				// spawns bird eggg at the current position (with y-1f)
				Instantiate(birdEgg, new Vector3(transform.position.x, 
					transform.position.y - 1f, transform.position.z), Quaternion.identity);
				attacked = true;
				anim.Play("BirdFly");
			}
		}
	}

	IEnumerator BirdDead(){
		yield return new WaitForSeconds(3f);
		gameObject.SetActive(false);
	}

	void OnTriggerEnter2D(Collider2D target){
		if(target.tag == Tags.BULLET_TAG){
			anim.Play("BirdDead");

			// setting as trigger allows bird to fall through the Ground 
			GetComponent<BoxCollider2D>().isTrigger = true;
			// lets bird fall down through gravity
			myBody.bodyType = RigidbodyType2D.Dynamic;

			canMove = false;
			StartCoroutine(BirdDead());
		}
	}
}
