using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestParticle : MonoBehaviour {

    public Vector3 velocity;
    public Vector3 acceleration;

    private Vector3 initialPosition;
    private TrailRenderer trailRenderer;

	// Use this for initialization
	void Start () {
        initialPosition = transform.position;
        trailRenderer = GetComponent<TrailRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		velocity += acceleration * Time.deltaTime;
		acceleration = Vector3.zero;
		gameObject.transform.position += velocity * Time.deltaTime;
	}

    public TrailRenderer GetTrailRenderer()
    {
        return trailRenderer;
    }

    public Vector3 GetInitialPosition()
    {
        return initialPosition;
    }
}
