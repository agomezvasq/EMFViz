using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : FieldGenerator {

    public double radius;
    public double internalRadius;
    public double angularSpeed;

	// Use this for initialization
	protected override void Start () {
        base.Start();

        ChargedObject = new ChargedRing(charge, transform.position, transform.rotation * Vector3.up, radius, internalRadius, angularSpeed);
    }
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();

        transform.rotation = Quaternion.Euler((transform.rotation.eulerAngles.y + (float)(((ChargedRing)ChargedObject).AngularSpeed * Time.deltaTime)) * Vector3.up);
    }
}
