using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerScript : MonoBehaviour {

	public int ID;
	public bool isSeated = false;
	private Transform[] children;

	public void UpdateCustomer(){
		children = transform.GetComponentsInChildren<Transform> ();
		//Choose a random, saturated, and bright random color.
		Color randCol = Random.ColorHSV (0f,1f,1f,1f,0.5f,1f,1f,1f);
		//Make all materials in the children of this customer that random color.
		foreach (Transform child in children) {
			if (child != this.transform) {
				child.GetComponent<Renderer> ().material.color = randCol;
			}
		}
	}
}
