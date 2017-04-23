using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : FieldGenerator {

    // Use this for initialization
    protected override void Start () {
        base.Start();
        
        ChargedObject = new ChargedSphere(charge, transform.position, transform.localScale.x / 2D);
        print("hello");
    }

    // Update is called once per frame
    protected override void Update () {
        base.Update();
	}
}
