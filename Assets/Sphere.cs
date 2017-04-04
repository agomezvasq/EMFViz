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

	public Vector3 ElectricField (Vector3 position) {
		Vector3 r = position - transform.position;
		return r.normalized * charge / r.sqrMagnitude;
	}

	public Vector3 MagneticField (Vector3 position) {
		Vector3 r = position - transform.position;
		return Vector3.Cross (velocity, r.normalized) * charge / r.sqrMagnitude;
	}

	public float ChargeDensity () {
		return charge / (4.0f * Mathf.PI * 0.25f);
	}

	public float b (float a) {
		return Mathf.Sqrt (1.0f - a * a);
	}

	public Vector3 SpinningMagneticField (Vector3 position, Vector3 angularVelocity) {
		Vector3 r = position - transform.position;
		float radius = r.magnitude;
		float a = 0.0625f * angularVelocity.magnitude * ChargeDensity () * 2.0f * r.z / (3.0f * Mathf.Pow (radius, 4.0f));
		float c = r.z / radius;
		Vector3 vector3 = new Vector3 (Mathf.Sin (b (c)), 0.0f, Mathf.Cos (b (c)));
		return a * vector3;
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
