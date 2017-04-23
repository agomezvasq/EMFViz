using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedRing : ChargedObject {

    private static double I(double charge, double angularSpeed)
    {
        return charge * Frequency(angularSpeed);
    }

    private static double Frequency(double angularSpeed)
    {
        return angularSpeed / (2D * Mathf.PI);
    }

    private static double CenterB(double current, double radius)
    {
        return EMField.PERMEABILITY * current / (2D * radius);
    }

    private static double AxisB(double current, double radius, double x)
    {
        double radiusSquared = radius * radius;
        double a = x * x + radiusSquared;
        return EMField.PERMEABILITY * radiusSquared * current / (2D * Mathf.Sqrt((float)(a * a * a)));
    }

    public override double Charge {
        get
        {
            return base.Charge;
        }
        set
        {
            _charge = value;
            Current = 1D;
            CenterMagneticField = 1D;
        }
    }

    protected Vector3 _normal;
    public Vector3 Normal
    {
        get
        {
            return _normal;
        }
        set
        {
            _normal = value;
        }
    }

    protected double _radius;
    public double Radius {
        get
        {
            return _radius;
        }
        set
        {
            _radius = value;
            Current = 1D;
            CenterMagneticField = 1D;
        }
    }

    protected double _internalRadius;
    public double InternalRadius
    {
        get
        {
            return _internalRadius;
        }
        set
        {
            _internalRadius = value;
        }
    }

    protected double _angularSpeed;
    public double AngularSpeed {
        get
        {
            return _angularSpeed;
        }
        set
        {
            _angularSpeed = value;
            Current = 1D;
            CenterMagneticField = 1D;
        }
    }

    protected double _current;
    protected double Current {
        get
        {
            return _current;
        }
        set {
            _current = I(Charge, AngularSpeed);
        }
    }

    protected double _centerMagneticField;
    protected double CenterMagneticField
    {
        get
        {
            return _centerMagneticField;
        }
        set
        {
            _centerMagneticField = CenterB(Current, Radius);
        }
    }

    public ChargedRing(double charge, Vector3 position, Vector3 normal, double radius, double internalRadius, double angularSpeed) : this(charge, position, Vector3.zero, normal, radius, internalRadius, angularSpeed) {}

    public ChargedRing(double charge, Vector3 position, Vector3 velocity, Vector3 normal, double radius, double internalRadius, double angularSpeed) : base(charge, position, velocity)
    {
        Radius = radius;
        InternalRadius = internalRadius;
        AngularSpeed = angularSpeed;
        Normal = normal;
    }

    public override Vector3 ElectricField(Vector3 position)
    {
        return Vector3.zero;
    }

    public override Vector3 MagneticField(Vector3 position)
    {
        if (Charge == 0D || AngularSpeed == 0D) {
            return Vector3.zero;
        }
        Vector3 relativePosition = position - Position;
        float relation = Vector3.Dot(Normal, relativePosition);
        Vector3 relativeNormal = Normal * relation;
        Vector3 relativeRadius = relativePosition - relativeNormal;
        double x = relativeNormal.magnitude * Sign(relation);
        double r = relativeRadius.magnitude;
        if (x == 0 || r == 0)
        {
            if (x == 0 && r == 0)
            {
                return (float)CenterMagneticField * Normal;
            }
            if (r == 0)
            {
                return (float)AxisB(Current, Radius, x) * Normal;
            }
        }
        double alpha = r / Radius;
        double beta = x / Radius;
        double gamma = x / r;
        double A = (1D + alpha) * (1D + alpha) + beta * beta;
        float b = Mathf.Sqrt(4f * (float)alpha / (float)A);
        double c = A - 4D * alpha;
        double alphaSquared = alpha * alpha;
        double betaSquared = beta * beta;
        float d = Mathf.PI * Mathf.Sqrt((float)A);
        double ei = EMField.EllipticIntegralI(b);
        double eii = EMField.EllipticIntegralII(b);
        float bnc = (float)(CenterMagneticField / d * (eii * (1D - alphaSquared - betaSquared) / c + ei));
        float brc = (float)(CenterMagneticField * gamma / d * (eii * (1D + alphaSquared + betaSquared) / c - ei));
        Vector3 bn = bnc * Normal;
        Vector3 br = brc * relativeRadius.normalized;
        return bn + br;
    }

    public static double Sign(float x)
    {
        return x / Mathf.Abs(x);
    }
}
