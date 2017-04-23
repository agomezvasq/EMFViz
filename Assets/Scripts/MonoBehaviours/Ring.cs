using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : FieldGenerator {
    
    public double angularSpeed;
    public Vector3 normal;

	// Use this for initialization
	protected override void Start () {
        lastPos = transform.parent.position;

        transform.parent.GetComponent<MeshRenderer>().material.color = GetColorFromCharge(charge);

        Torus torus = transform.parent.GetComponent<Torus>();
        float radius = torus.segmentRadius;
        float internalRadius = torus.segmentRadius - torus.tubeRadius * 2f;

        ChargedObject = new ChargedRing(charge, transform.parent.position, transform.parent.rotation * Vector3.up, radius, internalRadius, angularSpeed);
        print(((ChargedRing)ChargedObject).Normal);
    }
	
	// Update is called once per frame
	protected override void Update () {
        transform.parent.position += ChargedObject.Velocity * Time.deltaTime;

        if (charge != ChargedObject.Charge)
        {
            transform.parent.GetComponent<MeshRenderer>().material.color = GetColorFromCharge(ChargedObject.Charge);
            charge = ChargedObject.Charge;
        }

        float angle = (float)((ChargedRing)ChargedObject).AngularSpeed * Time.deltaTime;
        transform.parent.Rotate(((ChargedRing)ChargedObject).Normal, angle);
    }
}
