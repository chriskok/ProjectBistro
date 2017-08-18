using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Setup02Script : MonoBehaviour {

	public Button eraseButton;
	public Text moneyText;
	private Color initialEraseColor;
	private GameObject map;

	void Start(){
		initialEraseColor = eraseButton.colors.normalColor;
		if (GameManager.eraseMode == true) {
			eraseButton.GetComponent<Image>().color = Color.red;
		}

		if (SceneManager.GetActiveScene ().buildIndex == 2) {
			GameManager.MapSwitch (false);
		} else {
			GameManager.MapSwitch (true);
		}
	}

	void Update(){
		if (moneyText != null) {
			//GameManager.money = initialMoney - GameManager.itemAmount [0] * GameManager.itemPrices [0] - GameManager.itemAmount [1] * GameManager.itemPrices [1] - GameManager.itemAmount [2] * GameManager.itemPrices [2];
			moneyText.text = "Money: $" + GameManager.money;
		}
	}

	public void ChangeEraseMode (){
		if (GameManager.eraseMode == false) {
			GameManager.SetEraseMode (true);
			eraseButton.GetComponent<Image>().color = Color.red;
		} else if (GameManager.eraseMode == true) {
			GameManager.SetEraseMode (false);
			eraseButton.GetComponent<Image>().color = initialEraseColor;
		}
	}
}
