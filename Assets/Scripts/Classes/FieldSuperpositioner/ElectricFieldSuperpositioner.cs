using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricFieldSuperpositioner : FieldSuperpositioner {

    public override Vector3 Field(Vector3 position)
    {
        Vector3 electricField = Vector3.zero;
        foreach (FieldGenerator fieldGenerator in fieldGenerators)
        {
            electricField += fieldGenerator.ChargedObject.ElectricField(position);
        }
        return electricField;
    }
}
