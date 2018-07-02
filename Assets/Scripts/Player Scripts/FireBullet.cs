using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// defines movement of bullet
public class FireBullet : MonoBehaviour {

	private float speed = 10f;
	private Animator anim;

	private bool canMove;

	void Awake(){
		anim = GetComponent<Animator>();
	}

	void Start () {
		canMove = true;
		StartCoroutine(DisableBullet(5f));
	}
	
	void Update () {
		Move();
	}

	void Move(){
		if(canMove){
			Vector3 temp = transform.position;
			// advances the x-axis of bullet by (speed * time)
			temp.x += speed * Time.deltaTime;
			transform.position = temp;	
		}

	}

	public float Speed{
		get{
			return speed;
		}
		set{
			speed = value;
		}
	}

	// disables bullet after 'timer' seconds of being fired
	IEnumerator DisableBullet(float timer){
		yield return new WaitForSeconds(timer);
		gameObject.SetActive(false);
	}

	// disables bullet upon collision
	void OnTriggerEnter2D(Collider2D target){
		if(target.gameObject.tag == Tags.BEETLE_TAG || 
			target.gameObject.tag == Tags.SNAIL_TAG){
				anim.Play("Explode");
				canMove = false;
				StartCoroutine(DisableBullet(0.1f));
			}
	}
}
