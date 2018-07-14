using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneScript : MonoBehaviour {

	void Start () {
		// calls the function 'Deactivate' after 4 seconds
		Invoke("Deactivate", 4f);
	}

	void Deactivate(){
		gameObject.SetActive(false);
	}
	
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D target){
		// deals damage, and deactivates stone if it hits player
		if(target.tag == Tags.PLAYER_TAG){
			target.GetComponent<PlayerDamage>().DealDamage();
			gameObject.SetActive(false);
		}
	}
}
