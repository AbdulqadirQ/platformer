using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBlock : MonoBehaviour {

	public Transform bottom_collision;

	// is used only to set bonusblock's animation to 'BlockIdle'
	private Animator anim;

	public LayerMask playerLayer;

	private Vector3 moveDirection = Vector3.up;
	private Vector3 originPosition;
	private Vector3 animPosition;
	// NOTE: is called startAnim, but doesn't refer to an ACTUAL animtaion - it's
	//		 actually just a translation of the bonusblock object
	private bool startAnim;
	private bool canAnimate = true;

	void Awake(){
		anim = GetComponent<Animator>();
	}

	void Start () {
		originPosition = transform.position;
		animPosition = transform.position;
		// block will be moved up on collision with the player
		animPosition.y += 0.15f;
	}
	
	void Update () {
		CheckForCollision();
		AnimateUpDown();
	}

	void CheckForCollision(){
		if (canAnimate){
			// Collision is detected if player object is 0.1f within bottom_collision object of block
			RaycastHit2D hit = Physics2D.Raycast (
				bottom_collision.position, Vector2.down, 0.1f, playerLayer);

			// extra check probably isn't needed (since raycast already exists with playerLayer)
			if(hit){
				if(hit.collider.gameObject.tag == Tags.PLAYER_TAG){
					// increase score
					anim.Play("BlockIdle");
					startAnim = true;
					// only checks collision once, so we don't keep moving block downwards
					// even after it's already been used
					canAnimate = false;
				}
			}
		}

	}

	void AnimateUpDown(){
		if(startAnim){
			transform.Translate(moveDirection * Time.smoothDeltaTime);
			
			// block only translates upwards 0.15f before going back down
			if(transform.position.y >= animPosition.y){
				moveDirection = Vector3.down;
			// if block is back to original position, stop animation
			} else if(transform.position.y <= originPosition.y){
				startAnim = false;
			}
		}
	}
}
