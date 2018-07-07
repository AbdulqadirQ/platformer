using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderScript : MonoBehaviour {

	private Animator anim;
	private Rigidbody2D myBody;

	private Vector3 moveDirection = Vector3.down;

	private string change_movement_coroutine = "ChangeMovement";

	void Awake(){
		anim = GetComponent<Animator>();
		myBody = GetComponent<Rigidbody2D>();
	}

	void Start () {
		StartCoroutine(change_movement_coroutine);		
	}
	
	void Update () {
		MoveSpider();
	}

	void MoveSpider(){ 
		transform.Translate(moveDirection * Time.smoothDeltaTime);
	}

	IEnumerator ChangeMovement(){
		yield return new WaitForSeconds(Random.Range(2f, 5f));

		if(moveDirection == Vector3.down){
			moveDirection = Vector3.up;
		} else{
			moveDirection = Vector3.down;
		}

		// StartCoroutine is called with a string so the coroutine can be stopped
		// later on using StopCoroutine when the spider dies
		// Is recurive that's initially called in Start(), so will continuously 
		// execute until we don't want it to
		StartCoroutine(change_movement_coroutine);
	}

	IEnumerator SpiderDead(float timer){
		yield return new WaitForSeconds(timer);
		gameObject.SetActive(false);
	}

	void OnTriggerEnter2D(Collider2D target){
		if(target.tag == Tags.BULLET_TAG){
			anim.Play("SpiderDead");

			myBody.bodyType = RigidbodyType2D.Dynamic;

			StartCoroutine(SpiderDead(3f));
			// spider's direction change stops
			StopCoroutine(change_movement_coroutine);
		}
	} 
}
