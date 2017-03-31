using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave {

	private float magnitude;
	public float Magnitude {
		get {
			return magnitude;
		}
		set {
			magnitude = value;
		}
	}
	private float speed;
	public float Speed {
		get {
			return speed;
		}
		set {
			speed = value;
		}
	}

	public Wave(float magnitude, float speed) {
		Magnitude = magnitude;
		Speed = speed;
	}

	public float GetMagnitudeAt(float radius, float time) {
		return Magnitude / (radius * radius);
	}
}
