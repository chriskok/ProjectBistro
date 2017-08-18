using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
		for (int i = 0; i < 5; i++) {
			yield return new WaitForSeconds (2);
			os.AssignRandomSeat ();
		}
	}

	void Update(){
		moneyText.text = "Money: $" + GameManager.money;

		//Show the time in seconds
		timer += Time.deltaTime;
		string seconds = (timer % 60).ToString ("00");
		timeText.text = "Time: " + seconds;
	}
}
