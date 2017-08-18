using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderScript : MonoBehaviour {

	public List<Order> orderList;

	// Use this for initialization
	void Start () {
		orderList = new List<Order> (); 
	}

	void Update(){
		if (Input.GetKeyDown ("z")) {
			AddOrder(7,4);
		}
	}

	public class Order {
		//public int food;
		public int x;
		public int y;

		public Order(int x, int y){
			//this.food = food;
			this.x = x;
			this.y = y;
		}
	}

	public void AddOrder(int x, int y){

		//Formula to make the customer order the cheaper food item more often
		float[] percentages = new float[GameManager.foodPrices.Length];

		int totalPrice = 0;
		foreach (int f in GameManager.foodPrices) {
			totalPrice += f;
		}
		for (int i = 0; i < percentages.Length; i++) {
			percentages [i] = (GameManager.foodPrices [i] / (float)totalPrice); 
			//Debug.Log (i + ": " + percentages [i]);
		}

		float inversePercent = 0;
		foreach (float f in percentages) {
			inversePercent += 1 - f; 
		}
		for (int i = 0; i < percentages.Length; i++) {
			percentages [i] = ((1 - percentages [i]) / inversePercent); 
			//Debug.Log (i + ": " + percentages [i]);
		}

		float rand = Random.Range (0, 1f);

		if (rand < percentages [0]) {
			GameManager.foodLeft [0]--;
			GameManager.money += GameManager.foodPrices [0];
			Debug.Log ("Burger order recieved at " + x + "," + y + ". Burgers left: " + GameManager.foodLeft[0]);

		} else if (rand < percentages [0] + percentages [1]) {
			GameManager.foodLeft [1]--;
			GameManager.money += GameManager.foodPrices [1];
			Debug.Log ("Pasta order recieved at " + x + "," + y + ". Pastas left: " + GameManager.foodLeft[1]);
		} else if (rand < 1) {
			GameManager.foodLeft [2]--;
			GameManager.money += GameManager.foodPrices [2];
			Debug.Log ("Beverage order recieved at " + x + "," + y + ". Beverages left: " + GameManager.foodLeft[2]);
		}


		orderList.Add (new Order (x, y)); 
		//TODO: give the order to the closest free waiter. 
	}

	public void AssignRandomSeat(){
		List<ChairModelScript> c = new List<ChairModelScript> ();
		foreach (TileScript t in GameManager.mapArray) {
			if (t.it == ItemScript.itemType.chair) {
				ChairModelScript currentScript = t.GetComponentInChildren<ChairModelScript> ();
				if (currentScript.tableChosen && currentScript.TableHasSpace() && !currentScript.occupied) {
					c.Add (currentScript);
					//Debug.Log ("Adding this chair at " + t.x + ", " + t.y);
				}
			}
		}

		if (c.Count > 0) {
			//Choose a random unoccupied seat
			int randomSeat = Random.Range (0, c.Count);
			//Debug.Log ("Random seat index: " + randomSeat);
			while (c [randomSeat].occupied == true) {
				randomSeat = Random.Range (0, c.Count);
				//Debug.Log ("Next seat: " + randomSeat);
			}
				
			int randomCust = Random.Range (0, GameManager.custNumber);
			//Debug.Log ("Random customer index: " + randomCust);
			while (GameManager.custList [randomCust].GetComponent<CustomerScript> ().isSeated == true) {
				randomCust = Random.Range (0, GameManager.custNumber);
				//Debug.Log ("Next customer: " + randomCust);
			}


			Vector2 freeSpacePosition = c [randomSeat].GetFreeSpace ();
			GameManager.custList [randomCust].transform.position = c [randomSeat].transform.position;
			GameManager.custList [randomCust].transform.LookAt (c [randomSeat].tableModel.transform);
			c [randomSeat].occupied = true;
			GameManager.custList [randomCust].GetComponent<CustomerScript> ().isSeated = true;
			AddOrder ((int)freeSpacePosition.x, (int)freeSpacePosition.y);
		} else {
			Debug.Log ("No chairs to seat customers");
		}
	}
}
