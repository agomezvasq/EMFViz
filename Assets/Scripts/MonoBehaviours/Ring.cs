using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : FieldGenerator {
    
    public double angularSpeed;

	// Use this for initialization
	protected override void Start () {
        lastPos = transform.parent.position;

        transform.parent.GetComponent<MeshRenderer>().material.color = GetColorFromCharge(charge);

        Torus torus = transform.parent.GetComponent<Torus>();
        float radius = torus.segmentRadius;
        float internalRadius = torus.segmentRadius - torus.tubeRadius * 2f;

        ChargedObject = new ChargedRing(charge, transform.position, transform.rotation * Vector3.up, radius, internalRadius, angularSpeed);
    }
	
	// Update is called once per frame
	protected override void Update () {
        transform.position += ChargedObject.Velocity * Time.deltaTime;

        if (charge != ChargedObject.Charge)
        {
            transform.parent.GetComponent<MeshRenderer>().material.color = GetColorFromCharge(ChargedObject.Charge);
            charge = ChargedObject.Charge;
        }

        transform.rotation = Quaternion.Euler((transform.rotation.eulerAngles.y + (float)(((ChargedRing)ChargedObject).AngularSpeed * Time.deltaTime)) * Vector3.up);
    }
}
