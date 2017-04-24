using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {
    
	public Ring ring1;
	public Ring ring2;

	public FieldSuperpositioner fieldSuperpositioner;
	public TestParticleGrid testParticleGrid;

	// Use this for initialization
	void Start () {
        fieldSuperpositioner = new FieldSuperpositioner();
        fieldSuperpositioner.Add(ring1);
        fieldSuperpositioner.Add(ring2);
    }

	// Update is called once per frame
	void Update () {
        testParticleGrid.UpdateFields(fieldSuperpositioner);
	}
}
