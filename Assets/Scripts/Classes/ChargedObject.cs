using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChargedObject {

    protected double _charge;
	public virtual double Charge {
        get
        {
            return _charge;
        }
        set
        {
            _charge = value;
        }
    }

    protected Vector3 _position;
	public virtual Vector3 Position
    {
        get
        {
            return _position;
        }
        set {
            _position = value;
        }
    }

    protected Vector3 _velocity;
	public virtual Vector3 Velocity
    {
        get
        {
            return _velocity;
        }
        set
        {
            _velocity = value;
        }
    }

	public abstract Vector3 ElectricField (Vector3 position);
	public abstract Vector3 MagneticField (Vector3 position);

	public Vector3 ElectricForce (double charge, Vector3 position)
    {
        if (Charge == 0D || charge == 0D) {
            return Vector3.zero;
        }
        return (float)charge * ElectricField(position);
    }

    public Vector3 MagneticForce(double charge, Vector3 position, Vector3 velocity)
    {
        if (Charge == 0D || charge == 0D || velocity == Vector3.zero) {
            return Vector3.zero;
        }
        return (float)charge * Vector3.Cross(velocity, MagneticField(position));
    }

    public virtual Vector3 LorentzForce(double charge, Vector3 velocity, Vector3 position)
    {
        if (Charge == 0D || charge == 0D)
        {
            return Vector3.zero;
        }
        return (float)charge * (ElectricField(position) + Vector3.Cross(velocity, MagneticField(position)));
    }

	public ChargedObject (double charge, Vector3 position) : this (charge, position, Vector3.zero) {}

	public ChargedObject (double charge, Vector3 position, Vector3 velocity) {
		Charge = charge;
		Position = position;
		Velocity = velocity;
	}
}
