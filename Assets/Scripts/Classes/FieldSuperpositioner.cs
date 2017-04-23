using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldSuperpositioner {

	public List<FieldGenerator> fieldGenerators;

	public FieldSuperpositioner() {
        fieldGenerators = new List<FieldGenerator> ();
	}

	public void Add(FieldGenerator fieldGenerator) {
        fieldGenerators.Add(fieldGenerator);
	}

	public FieldGenerator Get(int index) {
		return fieldGenerators[index];
	}

	public Vector3 ElectricField(Vector3 position) {
		Vector3 electricField = Vector3.zero;
		foreach (FieldGenerator fieldGenerator in fieldGenerators) {
			electricField += fieldGenerator.ChargedObject.ElectricField (position);
		}
		return electricField;
	}

	public Vector3 MagneticField(Vector3 position) {
		Vector3 magneticField = Vector3.zero;
		foreach (FieldGenerator fieldGenerator in fieldGenerators) {
			magneticField += fieldGenerator.ChargedObject.MagneticField (position);
		}
		return magneticField;
	}
}
