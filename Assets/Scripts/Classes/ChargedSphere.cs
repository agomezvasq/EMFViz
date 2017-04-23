using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedSphere : ChargedObject {

	private static double E (double charge, double radius) {
		return EMField.K * charge / (radius * radius);
	}

    private static double B (double charge, double speed, double radius) {
		return EMField.H * charge * speed / (radius * radius);
	}

	private double _radius;
	public double Radius { get { return _radius; } set {  _radius = value; } }

	public ChargedSphere (double charge, Vector3 position, double radius) : this(charge, position, Vector3.zero, radius) {}

	public ChargedSphere (double charge, Vector3 position, Vector3 velocity, double radius) : base(charge, position, velocity) {
		Radius = radius;
	}

	public override Vector3 ElectricField (Vector3 position) {
		if (Charge == 0D) {
			return Vector3.zero;
		}
		Vector3 relativePosition = position - Position;
		Vector3 u = relativePosition.normalized;
		return u * (float)E (Charge, relativePosition.magnitude);
	}

	public override Vector3 MagneticField (Vector3 position) {
		if (Charge == 0D || Velocity == Vector3.zero) {
			return Vector3.zero;
		}
		Vector3 relativePosition = position - Position;
		Vector3 u = relativePosition.normalized;
		Vector3 n = Vector3.Cross (Velocity.normalized, u);
		return n * (float)B (Charge, Velocity.magnitude, relativePosition.magnitude);
	}
    
	public override Vector3 LorentzForce (double charge, Vector3 velocity, Vector3 position)
	{
		if (Charge == 0D || charge == 0D) {
			return Vector3.zero;
		}
		return (float)charge * (ElectricField (position) + Vector3.Cross (velocity, MagneticField (position)));
	}
}
