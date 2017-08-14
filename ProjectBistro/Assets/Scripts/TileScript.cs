﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour {

	private bool chosen = false;
	private Material mat;
	private Color baseColor;
	private GameObject model;
	private GameObject currentModel;
	private float yOffset;

	public Color highlightColor;
	public Color chosenColor;

	// Use this for initialization
	void Start () {
		mat = GetComponent<Renderer>().material;
		baseColor = mat.color;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseOver(){
		if (!chosen) {
			mat.color = highlightColor;
		}
	}

	void OnMouseExit(){
		mat.color = baseColor;
	}

	//Set the item chosen to be displayed on this tile
	//TODO: use different int values to represent different items in parantheses
	public void SetItem(){
		mat.color = chosenColor;
		baseColor = chosenColor;
		chosen = true;
		currentModel = Instantiate (this.model, new Vector3(transform.position.x, yOffset, transform.position.z), model.transform.rotation);
	}

	public void SetModel(GameObject g){
		this.model = g;
	}

	public void SetYOffset(float yOff){
		this.yOffset = yOff;
	}
}
