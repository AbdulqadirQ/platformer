using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // import manually added for UI script

public class ScoreScript : MonoBehaviour {

	private Text coinTextScore;
	private AudioSource audioManager;
	private int scoreCount;

	void Awake(){
		audioManager = GetComponent<AudioSource>();
	}

	void Start () {
		// GameObject.Find retreives the Game Object based the name of an object within heirarchy
		coinTextScore = GameObject.Find("CoinText").GetComponent<Text>();

	}

	void OnTriggerEnter2D(Collider2D target){
		if(target.tag == Tags.COIN_TAG){
			target.gameObject.SetActive(false);
			scoreCount++;

			// sets the text of the score
			coinTextScore.text = "x" + scoreCount;
			audioManager.Play();
		}
	}
}
