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
	public Text dayText;

	public float timePerDay;
	public float timePerSeating;
	float timer = 0;

	void Start(){
		StartCoroutine (seatRandomCust());
		os = GameObject.FindGameObjectWithTag ("OrderHandler").GetComponent<OrderScript> ();
		if (GameManager.day < 10) {
			dayText.text = "Day 0" + GameManager.day;
		} else {
			dayText.text = "Day " + GameManager.day;
		}
	}

	IEnumerator seatRandomCust(){
		while (true){
			yield return new WaitForSeconds (timePerSeating);
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
		if (timer > timePerDay) {
			Debug.Log ("Day Over!");
			GameManager.day++;
			SceneManager.LoadScene (1);
		}
	}
}
