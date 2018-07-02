using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

	public GameObject fireBullet;

	void Update(){
		ShootBullet();
	}

	void ShootBullet(){
		// true if key is pressed
		if(Input.GetKeyDown(KeyCode.Mouse1)){
			// duplicates bullet. Quaternion.identity = 0 rotation applied
			GameObject bullet = Instantiate(fireBullet, transform.position, 
				Quaternion.identity);
			// bullet will fire on the side which the player is facing, since this script
			// is attached to the player, and we're using the player's transform to calculate
			// the bullet's transform each frame
			bullet.GetComponent<FireBullet>().Speed *= transform.localScale.x;
		}
	}
}
