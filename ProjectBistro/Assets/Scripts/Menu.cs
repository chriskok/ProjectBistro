using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//For SceneManager
using UnityEngine.SceneManagement;
//for Text
using UnityEngine.UI;
//for serializable
using System;

public class Menu : MonoBehaviour {
	int currentFoodChoice;
	public Button[] FoodButtons;
	public Slider[] Setpro; 
	public Text[] SetproText;
	public bool[] OffSetFood;
	public InputField inQuanity;
	public Text reminder;
	private int Budget;
	private Food FastFood = new Food(0.15f,0);
	private Food Italian = new Food(0.25f,0);
	private Food Beverage = new Food(0.15f,0);
	private Food[] foods;
	int totalCost;
	public Text FoodText;
	public Text CQ;
	public Text Cost;
	public Text TCost;
	public Text BudgetText;
	//class for food contain quanity and cost
	[Serializable]
	public class Food
	{
		public int tempQuan;
		int price;
		int quality;
		int size;
		int lastQuanity;
		int quanity;
		float costRate;
		int cost;

		public Food(float costRate,int lastquan){
			price = 1;
			size = 1;
			lastQuanity = lastquan;
			quanity = 0;
			quality = 30;
			this.costRate = costRate;
			cost = (int)(costRate*quality);
		}
		public void AddQuan(int quan){
			quanity += quan;
		}
		private void UpdateCost(){
			cost = (int)(costRate*quality);
		}
		public void SetSize(int size){
			this.size = size;
		}
		public void SetQual(int quality){
			this.quality = quality;
			UpdateCost ();
		}
		public void SetPrice(int price){
			this.price = price;
		}
		public void SetLastQuantity(int LQ){
			this.lastQuanity = LQ;
		}
		public int GetQuan(){
			return quanity;
		}
		public int GetSize(){
			return size;
		}
		public int GetQual(){
			return quality;
		}
		public int GetCost(){
			return cost;
		}
		public int Getprice(){
			return price;
		}
		public int GetLQ(){
			return lastQuanity;
		}
	}

	void SetBudgetText(){
		BudgetText.text = "Money: \t\t\t" +Budget.ToString();
	}
	private void OffInteractable(){
		for(int i =0;i<Setpro.Length;i++){
			Setpro [i].interactable = false;
		}
	}
	private void OnInteractable(){
		for(int i =0;i<Setpro.Length;i++){
			Setpro [i].interactable = true;
		}
	}
	public void Start(){
		OffInteractable ();
		currentFoodChoice = -1;
		Budget = GameManager.money;
		CQ.text = "Old Quantity: ";
		Cost.text = "Cost: ";
		TCost.text = "Total Cost: ";
		SetBudgetText();
		OffSetFood = new bool[3];
		for (int i = 0; i < 3; i++) {
			OffSetFood [i] = false;
		}
		Food []tempFoods={FastFood,Italian,Beverage};
		foods = tempFoods;
		tempFoods = null;

		//Update last quantity when the scene starts
	}

	public void Back(){
		//SceneManager.LoadScene ("");
	}
	public void Next(){
		//SceneManager.LoadScene ();
	}

	public void Buy(){
		int inQ = -1;
		Int32.TryParse(inQuanity.text, out inQ);
		if (inQ >0) {
			if (Budget - ((foods [currentFoodChoice]).GetCost () * inQ) < 0) {
				reminder.text = "You are out of Budget!";
			} else {
				(foods[currentFoodChoice]).AddQuan (inQ);
				SetproText[3].text =((foods[currentFoodChoice]).GetQuan ()).ToString ();
				Budget -= ((foods [currentFoodChoice]).GetCost () * inQ);
				OffSetFood [currentFoodChoice] = true;
				SetBudgetText ();
				OffInteractable ();

				GameManager.foodAmount[currentFoodChoice] += inQ;
				GameManager.money -= (foods [currentFoodChoice]).GetCost () * inQ;
			}
		}
	}

	private void FoodUpdate(){
		Setpro [0].value = foods[currentFoodChoice].Getprice ();
		Setpro [1].value = foods[currentFoodChoice].GetSize ();
		Setpro [2].value = foods[currentFoodChoice].GetQual ();
		for (int i = 0; i< SetproText.Length; i++) {
			SetproTextMethod (i);
		}

		foods [currentFoodChoice].SetLastQuantity (GameManager.foodAmount [currentFoodChoice]);
		CQ.text = "Old Quantity: \t" +foods[currentFoodChoice].GetLQ().ToString();
	}

	private void SetproTextMethod(int index){
		switch(index){
		case 0:
			SetproText [index].text = foods[currentFoodChoice].Getprice().ToString ();
			break;
		case 1:
			SetproText [index].text = foods[currentFoodChoice].GetSize().ToString ();
			break;
		case 2:
			SetproText [index].text = foods[currentFoodChoice].GetQual().ToString ();
			break;
		case 3:
			SetproText [index].text = foods[currentFoodChoice].GetQuan ().ToString ();
			break;
		}
	}
	public void TotalCostUpdate(){
		int inQ = -1;
		Int32.TryParse(inQuanity.text, out inQ);
		if (inQ > 0) {
			foods[currentFoodChoice].tempQuan = inQ;
			TCost.text = "Total Cost: \t\t\t" + (foods[currentFoodChoice].GetCost()*inQ).ToString();
		}
	}
	public void SetFastFood(){
		currentFoodChoice = 0;
		FoodText.text = "Fast Food";
		FoodUpdate();
		UpdateCost();
		TotalCostUpdate ();
		if (OffSetFood [0] == false) {
			OnInteractable ();
		} else {
			OffInteractable ();
		}
	}
	public void SetItalian(){
		currentFoodChoice = 1;
		FoodText.text = "Italian";
		FoodUpdate ();
		UpdateCost ();
		TotalCostUpdate ();
		if (OffSetFood [1] == false) {
			OnInteractable ();
		} else {
			OffInteractable ();
		}
	}
	public void SetBeverage(){
		currentFoodChoice = 2;
		FoodText.text = "Beverage";
		FoodUpdate ();
		UpdateCost ();
		TotalCostUpdate ();
		if (OffSetFood [2] == false) {
			OnInteractable ();
		} else {
			OffInteractable ();
		}
	}
	public void SetPrice(){
		int price = (int)Setpro [0].value;
		foods[currentFoodChoice].SetPrice(price);
		GameManager.foodPrices [currentFoodChoice] = price;
		SetproTextMethod (0);
	} 

	public void SetSize(){
		int size = (int)Setpro [1].value;
		foods[currentFoodChoice].SetSize(size);
		SetproTextMethod (1);
	}

	public void UpdateCost(){
		int quality = (int)Setpro [2].value;
		(foods[currentFoodChoice]).SetQual((int)quality);
		SetproTextMethod (2);
		Cost.text = "Cost: \t\t\t\t\t" +((foods[currentFoodChoice]).GetCost()).ToString();
		TotalCostUpdate();
	}
}
