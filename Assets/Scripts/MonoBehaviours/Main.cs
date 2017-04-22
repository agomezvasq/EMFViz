using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

	private static bool ARROWS = false;

	public Sphere sphere1;
	public Sphere sphere2;
	public ParticleList particleList;
	public TestParticleField eTestParticleField;
	public TestParticleField eTestParticleField1;
	public TestParticleField mTestParticleField;

	// Use this for initialization
	void Start () {
		particleList = new ParticleList ();
		particleList.Add (sphere1);
		particleList.Add (sphere2);
	}

	// Update is called once per frame
	void Update () {
		for (int i = 0; i < eTestParticleField.Rows(); i+=16) {
			for (int j = 0; j < eTestParticleField.Columns(); j+=16) {
				for (int k = 0; k < eTestParticleField.Aisles (); k += 16) {
					Vector3 position = eTestParticleField.Get (i, j, k).transform.position;

                    Vector3 initialPosition = eTestParticleField.initialPositions [i, j, k];
					Vector3 spinningMagneticField = particleList.SpinningMagneticField (position);

                    eTestParticleField.Get(i, j, k).velocity = spinningMagneticField;
				}
			}
		}
	}
}
