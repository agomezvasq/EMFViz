using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestParticle : MonoBehaviour {

	public Vector3 velocity;
	public Vector3 acceleration;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		velocity += acceleration * Time.deltaTime;
		acceleration = Vector3.zero;
		gameObject.transform.position += velocity * Time.deltaTime;
	}
}
