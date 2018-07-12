using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggScript : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D target){
		if(target.gameObject.tag == Tags.PLAYER_TAG){
			target.gameObject.GetComponent<PlayerDamage>().DealDamage();
		}
		StartCoroutine(DisableEgg(0.2f));
	}

	IEnumerator DisableEgg(float timer){
		yield return new WaitForSeconds(timer);
		gameObject.SetActive(false); 
	}
}
