using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour {

	public GameObject stone;
	// the point from which stone would spawn and be launched at player
	public Transform attackInstantiate;

	private Animator anim;

	private string attack_coroutine = "StartAttack";

	void Awake(){
		anim = GetComponent<Animator>();
	}

	void Start () {
		StartCoroutine(attack_coroutine);
	}
	
	void Attack(){
		GameObject obj = Instantiate(stone, attackInstantiate.position, Quaternion.identity);
		// force is added to the stone once initiated (-ve values due to Scale)
		obj.GetComponent<Rigidbody2D>().AddForce(
			new Vector2(Random.Range(-300f, -700f), 0f));
	}

	// is called from an animation event (at the last frame of BossAttack animation)
	void BackToIdle(){
		anim.Play("BossIdle");
	}

	public void DeactivateBossScript(){
		StopCoroutine(attack_coroutine);
		// disables current component (i.e. deactivates BossScript)
		enabled = false;
	}

	IEnumerator StartAttack(){
		yield return new WaitForSeconds(Random.Range(2f,5f));

		anim.Play("BossAttack");
		StartCoroutine(attack_coroutine);
	}
}
