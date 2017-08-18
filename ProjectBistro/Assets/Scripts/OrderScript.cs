using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class OrderScript : MonoBehaviour {

	public List<Order> orderList;

	public float timeToLeave;

	// Use this for initialization
	void Start () {
		orderList = new List<Order> (); 
	}

	void Update(){
	}

	public class Order {
		public int food;
		public int x;
		public int y;
		public ChairModelScript c;
		public int custID;

		public Order(int food, int x, int y, ChairModelScript c, int custID){
			this.food = food;
			this.x = x;
			this.y = y;
			this.c = c;
			this.custID = custID;
		}
	}

	public void AddOrder(int x, int y, ChairModelScript c, int custID){

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
			GameManager.foodAmount [0]--;

			orderList.Add (new Order (0, x, y, c, custID)); 
			Debug.Log ("Burger order recieved at " + x + "," + y + ". Burgers left: " + GameManager.foodAmount[0]);

		} else if (rand < percentages [0] + percentages [1]) {
			GameManager.foodAmount [1]--;
			//StartCoroutine (DeassignSeat (1, c, custID));
			orderList.Add (new Order (1, x, y, c, custID)); 
			Debug.Log ("Pasta order recieved at " + x + "," + y + ". Pastas left: " + GameManager.foodAmount[1]);
		} else if (rand < 1) {
			GameManager.foodAmount [2]--;
			//StartCoroutine (DeassignSeat (2, c, custID));
			orderList.Add (new Order (2, x, y, c, custID)); 
			Debug.Log ("Beverage order recieved at " + x + "," + y + ". Beverages left: " + GameManager.foodAmount[2]);
		}
			
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
			c [randomSeat].occupied = true;

			GameManager.custList [randomCust].transform.position = c [randomSeat].transform.position;
			GameManager.custList [randomCust].transform.LookAt (c [randomSeat].tableModel.transform);
			GameManager.custList [randomCust].GetComponent<CustomerScript> ().isSeated = true;

			AddOrder ((int)freeSpacePosition.x, (int)freeSpacePosition.y, c[randomSeat], randomCust);
		} else {
			Debug.Log ("No chairs to seat customers");
		}
	}

	public IEnumerator DeassignSeat(int foodType, ChairModelScript c, int custID){

		yield return new WaitForSeconds (timeToLeave);

		// Send the customer back to the waiting zone and make the chair unoccupied
		GameManager.custList [custID].transform.position = new Vector3 (31f,0,0);
		GameManager.custList [custID].GetComponent<CustomerScript> ().isSeated = false;
		c.occupied = false;

		//Pay the player for the type of food purchased 
		GameManager.money += GameManager.foodPrices [foodType];
	}
}
