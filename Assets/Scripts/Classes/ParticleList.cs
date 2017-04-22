using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleList {

	public List<Sphere> particles;

	public ParticleList () {
		particles = new List<Sphere> ();
	}

	public void Add (Sphere sphere) {
		particles.Add (sphere);
	}

	public Sphere Get (int index) {
		return particles [index];
	}

	public Vector3 ElectricField (Vector3 position) {
		Vector3 electricField = Vector3.zero;
		foreach (Sphere sphere in particles) {
			electricField += sphere.ElectricField (position);
		}
		return electricField;
	}

	public Vector3 MagneticField (Vector3 position) {
		Vector3 magneticField = Vector3.zero;
		foreach (Sphere sphere in particles) {
			magneticField += sphere.MagneticField (position);
		}
		return magneticField;
	}

	public Vector3 SpinningMagneticField (Vector3 position) {
		float angularSpeed = 100f;
		Vector3 spinningMagneticField = Vector3.zero;
		foreach (Sphere sphere in particles) {
			spinningMagneticField += sphere.SpinningMagneticField (position, angularSpeed);
		}
		return spinningMagneticField;
	}

	public Vector3 SpinningMagneticField1 (Vector3 position) {
		float angularSpeed = 100f;
		Vector3 spinningMagneticField = Vector3.zero;
		foreach (Sphere sphere in particles) {
			spinningMagneticField += sphere.SpinningMagneticField1 (position, angularSpeed);
		}
		return spinningMagneticField;
	}
}
