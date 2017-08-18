using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	//Static variables that we access throughout the game
	public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
	public static TileScript[,] mapArray; 
	public static Vector2 mouseOver;

	//Keeping track of tile variables
	public static int allocatedTiles = 50;
	public static int chosenTiles = 0;
	public static bool eraseMode = false;

	public static MapGenerator mg = null;

	//Customer Instantiation
	public static int custNumber = 30;
	public static List<GameObject> custList; 
	public GameObject customerPrefab;

	//Game variables that the user wants to keep track of
	public static int money = 1000;
	public static int[] foodPrices = { 0, 0, 0 }; //TODO: Let the player choose these values
	public static int[] foodAmount = { 0, 0, 0 };

	//Amount and Price of Tables, Chairs and Waiters Consecutively
	public static int[] itemAmount = {0,0,0};
	public static int[] itemPrices = { 100, 50, 200 }; //TODO: Make waiters price go up with amount

	private bool mapGenerated = false;

	void Awake(){
		//Check if instance already exists
		if (instance == null)
			//if not, set instance to this
			instance = this;

		//If instance already exists and it's not this:
		else if (instance != this)
			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);  
		
		DontDestroyOnLoad (gameObject);
	}


	// Use this for initialization
	void Start () {
		mg = GameObject.FindGameObjectWithTag ("Map").GetComponent<MapGenerator>();

		if (!mapGenerated) {
			mg.GenerateMap ();
			mapGenerated = true;
		}

		custList = new List<GameObject> ();
		for (int i = 0; i < custNumber; i++) {
			//Vector3 tilePosition = new Vector3(-mapSize.x/2 +0.5f + x, 0, -mapSize.y/2 + 0.5f + y);

			//Instantiate new tiles for every x and y value and set it to 0.1 scale to fit 1 unit square
			Vector3 offSite = new Vector3(-30, 0, 0); //Ofset tile location by 0.5f
			GameObject cust = Instantiate (customerPrefab, offSite, Quaternion.identity) as GameObject; 
			cust.transform.parent = this.transform;

			cust.GetComponent<CustomerScript> ().ID = i;
			cust.GetComponent<CustomerScript> ().UpdateCustomer ();
			custList.Add (cust);
		}
	}

	// Update is called once per frame
	void Update () {
		UpdateMouseOver ();
		//Debug.Log (mouseOver);

		//If we are in the first scene and erase mode is off, we can add tiles
		if (SceneManager.GetActiveScene ().buildIndex == 0 && !eraseMode) {
			if (Input.GetMouseButton (0) && mouseOver.x >= 0 && chosenTiles < allocatedTiles) {
				//Debug.Log ("chosen tile at " + mouseOver.x + ", " + mouseOver.y);
				if (mapArray [(int)mouseOver.x, (int)mouseOver.y].ChooseTile () == true){
					chosenTiles++;
				}
			}
		}
		//If we are in the first/second scene and erase mode is on, we can remove tiles
		if ((SceneManager.GetActiveScene ().buildIndex == 0 || SceneManager.GetActiveScene ().buildIndex == 1 ) && eraseMode) {
			if (Input.GetMouseButton (0) && mouseOver.x >= 0 && chosenTiles > 0) {
				//Debug.Log ("chosen tile at " + mouseOver.x + ", " + mouseOver.y);
				if (mapArray [(int)mouseOver.x, (int)mouseOver.y].UnchooseTile () == true){
					chosenTiles--;
				}
			}
		}
	}

	//Method to check the location of the mouse on screen in terms of x and y
	private void UpdateMouseOver(){
		if (!Camera.main) {
			Debug.Log ("Unable to find main camera");
			return;
		}

		RaycastHit hit; 
		if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 25.0f, LayerMask.GetMask ("Item"))) {
			mouseOver.x = Mathf.Floor(hit.point.x);
			mouseOver.y = Mathf.Floor(hit.point.z); //note that it is z because it's in 3D
		} else {
			mouseOver.x = -1;
			mouseOver.y = -1;
		}
	}

	public static int GetTiles(){
		return chosenTiles;
	}

	public static void SetEraseMode(bool em){
		eraseMode = em;
	}

	public static void MapSwitch(bool turnOn){
		if (turnOn) {
			mg.gameObject.SetActive (true);
		} else {
			mg.gameObject.SetActive (false);
		}
	}
}
