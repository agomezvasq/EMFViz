using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {
    
	public Sphere sphere1;
	public Sphere sphere2;

	public FieldSuperpositioner fieldSuperpositioner;
	public TestParticleGrid testParticleGrid;

	// Use this for initialization
	void Start () {
        fieldSuperpositioner = new FieldSuperpositioner();
        fieldSuperpositioner.Add(sphere1);
        fieldSuperpositioner.Add(sphere2);
	}

	// Update is called once per frame
	void Update () {
        testParticleGrid.UpdateFields(fieldSuperpositioner);
	}
}
