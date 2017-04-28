using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticFieldSuperpositioner : FieldSuperpositioner {

    public override Vector3 Field(Vector3 position)
    {
        Vector3 magneticField = Vector3.zero;
        foreach (FieldGenerator fieldGenerator in fieldGenerators)
        {
            magneticField += fieldGenerator.ChargedObject.MagneticField(position);
        }
        return magneticField;
    }
}
