using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainScript : MonoBehaviour {

	OrderScript os;

	//For UI 
	public Text moneyText;
	public Text timeText;
	float timer = 0;

	void Start(){
		StartCoroutine (seatRandomCust());
		os = GameObject.FindGameObjectWithTag ("OrderHandler").GetComponent<OrderScript> ();
	}

	IEnumerator seatRandomCust(){
		while (true){
			yield return new WaitForSeconds (4);
			os.AssignRandomSeat ();
		}
	}

	void Update(){
		moneyText.text = "Money: $" + GameManager.money;

		//Show the time in seconds
		timer += Time.deltaTime;
		string seconds = (timer % 60).ToString ("00");
		timeText.text = "Time: " + seconds;

		//TODO: set public var for timePerDay and timePerSeat
		//Time per day for now is 60
		if (timer > 60f) {
			Debug.Log ("Day Over!");
			SceneManager.LoadScene (1);
		}
	}
}
