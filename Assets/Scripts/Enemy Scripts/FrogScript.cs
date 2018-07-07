using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogScript : MonoBehaviour {

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
	
	void Update () {
		
	}

	IEnumerator FrogJump(){
		yield return new WaitForSeconds(Random.Range(1f, 4f));

		if(jumpLeft){
			anim.Play("FrogJumpLeft");
		}else{

		}

		StartCoroutine(frog_jump_coroutine);
	}

	// this function isn't called within the script. It's called directly from 
	// Unity. The animation 'FrogJumpLeft' has an event which calls a particular 
	// function at a particular frame. It's used here so at the last frame of 'FrogJumpLeft',
	// the frog is set to an idle animation, rather than repeating 'FrogJumpLeft'
	void AnimationFinished(){
		anim.Play("FrogIdleLeft");
	}
}
