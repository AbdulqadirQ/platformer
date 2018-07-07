using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogScript : MonoBehaviour {
	// FIXME: FrogJumpLeft animation jumps over x-axis further than FrogJumpRight 
	private Animator anim;

	private bool animation_Started;
	private bool animation_Finished;

	private int jumpedTimes;
	private bool jumpLeft = true;

	private string frog_jump_coroutine = "FrogJump";

	void Awake(){
		anim = GetComponent<Animator>();
	}

	void Start () {
		StartCoroutine(frog_jump_coroutine);
	}
	
	// is called at the end of each frame, i.e. after the Update()
	void LateUpdate () {
		// if animation has finished AND started
		if(animation_Finished && animation_Started){
			animation_Started = false; // only set so this if-statement is executed again

			// sets parent game object (Frog Parent) to equal the current transform
			transform.parent.position = transform.position;

			// sets the 'Frog' game object (child) to the current vector (i.e. after 
			// the animation has been played, therefore recording the end animation's position)
			transform.localPosition = Vector3.zero;
		}
	}

	IEnumerator FrogJump(){
		yield return new WaitForSeconds(Random.Range(1f, 4f));

		animation_Started = true;
		animation_Finished = false;

		jumpedTimes++;

		if(jumpLeft){
			anim.Play("FrogJumpLeft");
		}else{
			anim.Play("FrogJumpRight");
		}

		StartCoroutine(frog_jump_coroutine);
	}

	// this function isn't called within the script. It's called directly from 
	// Unity. The animation 'FrogJumpLeft' has an event which calls a particular 
	// function at a particular frame. It's used here so at the last frame of 'FrogJumpLeft',
	// the frog is set to an idle animation, rather than repeating 'FrogJumpLeft'
	void AnimationFinished(){
		animation_Finished = true;

		if(jumpLeft){
			anim.Play("FrogIdleLeft");
		}else{
			anim.Play("FrogIdleRight");
		}

		// frog will jump left 3 times, before jumping right 3 times, and so on
		if(jumpedTimes == 3){
			jumpedTimes = 0;

			Vector3 tempScale = transform.localScale;
			// switch to opposite scale (direction)
			tempScale.x *= -1;
			transform.localScale = tempScale;

			jumpLeft = !jumpLeft;
		}
	}
}
