using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour {

	private Text lifeText;
	private int lifeScoreCount;
	
	private bool canDamage;

	void Awake(){
		lifeText = GameObject.Find("LifeText").GetComponent<Text>();
		lifeScoreCount = 3;
		lifeText.text = "x" + lifeScoreCount;

		canDamage = true;
	}

	void Start(){
		// setting this to 1f since we will set it to 0f when player dies (below)
		Time.timeScale = 1f;
	}

	public void DealDamage(){
		if(canDamage){
			lifeScoreCount--;

			// don't want to display negative lives
			if(lifeScoreCount >= 0){
				lifeText.text = "x" + lifeScoreCount;
			}

			if(lifeScoreCount == 0){
				//stops and restarts game
				Time.timeScale = 0f;
				StartCoroutine(RestartGame());
			}

			canDamage = false;

			StartCoroutine(WaitForDamage());
		}
	}

	IEnumerator WaitForDamage(){
		yield return new WaitForSeconds(2f);
		canDamage = true;
	}

	IEnumerator RestartGame(){
		// using WaitForSecondsRealtime since we'll be stopping in-game time using
		// Time.timescale = 0f;
		yield return new WaitForSecondsRealtime(2f);
		SceneManager.LoadScene("Gameplay");
	}
}
