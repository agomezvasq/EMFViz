/**
 * Based on a script by Steffen (http://forum.unity3d.com/threads/torus-in-unity.8487/) (in $primitives_966_104.zip, originally named "Primitives.cs")
 *
 * Editted by Michael Zoller on December 6, 2015.
 * It was shortened by about 30 lines (and possibly sped up by a factor of 2) by consolidating math & loops and removing intermediate Collections.
 */
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Torus : FieldGenerator {

	public float segmentRadius = 1f;
	public float tubeRadius = 0.1f;
	public int numSegments = 32;
	public int numTubes = 12;

    public double angularSpeed;
    public Vector3 normal;

    protected override void Start() {
        GetComponent<MeshFilter>().mesh = RingCreator.CreateRing(segmentRadius, tubeRadius, numSegments, numTubes);

        lastPos = transform.parent.position;

        transform.parent.GetComponent<MeshRenderer>().material.color = GetColorFromCharge(charge);

        Torus torus = transform.parent.GetComponent<Torus>();
        float radius = torus.segmentRadius;
        float internalRadius = torus.segmentRadius - torus.tubeRadius * 2f;

        ChargedObject = new ChargedRing(charge, transform.parent.position, transform.parent.rotation * Vector3.up, radius, internalRadius, angularSpeed);
    }

    // Update is called once per frame
    protected override void Update()
    {
        transform.parent.position += ChargedObject.Velocity * Time.deltaTime;

        if (charge != ChargedObject.Charge)
        {
            transform.parent.GetComponent<MeshRenderer>().material.color = GetColorFromCharge(charge);
            ChargedObject.Charge = charge;
        }
        if (angularSpeed != ((ChargedRing)ChargedObject).AngularSpeed)
        {
            ((ChargedRing)ChargedObject).AngularSpeed = angularSpeed;
        }

        float angle = (float)((ChargedRing)ChargedObject).AngularSpeed * Time.deltaTime;
        transform.parent.Rotate(((ChargedRing)ChargedObject).Normal, angle, Space.World);
    }
}
