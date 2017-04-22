using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour {

	public float charge;
	public Vector3 velocity;

	private Vector3 lastPos;

	// Use this for initialization
	void Start () {
		lastPos = gameObject.transform.position;

		Color color = Color.black;
		if (charge > 0) {
			color = Color.red;
		} 
		else if (charge == 0) {
			color = Color.white;
		} 
		else {
			color = Color.cyan;
		}
		GetComponent<MeshRenderer> ().material.color = color;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += velocity * Time.deltaTime;
	}
    
	void OnMouseDrag () {
		Vector3 screenSpacePosition = Camera.main.WorldToScreenPoint (transform.position);
		Vector3 newPosition = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenSpacePosition.z));
		if (newPosition != transform.position) {
			transform.position = newPosition;
			velocity = (transform.position - lastPos) / Time.deltaTime;
			lastPos = transform.position;
		} 
		else {
			velocity = Vector3.zero;
		}
	}
}
