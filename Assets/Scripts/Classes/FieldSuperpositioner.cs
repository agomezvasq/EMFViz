using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldSuperpositioner {

	public List<IFieldGenerator> fieldGenerators;

	public FieldSuperpositioner() {
        fieldGenerators = new List<IFieldGenerator> ();
	}

	public void Add(IFieldGenerator fieldGenerator) {
        fieldGenerators.Add(fieldGenerator);
	}

	public IFieldGenerator Get(int index) {
		return fieldGenerators[index];
	}

	public Vector3 ElectricField(Vector3 position) {
		Vector3 electricField = Vector3.zero;
		foreach (IFieldGenerator fieldGenerator in fieldGenerators) {
			electricField += fieldGenerator.ChargedObject.ElectricField (position);
		}
		return electricField;
	}

	public Vector3 MagneticField(Vector3 position) {
		Vector3 magneticField = Vector3.zero;
		foreach (IFieldGenerator fieldGenerator in fieldGenerators) {
			magneticField += fieldGenerator.ChargedObject.MagneticField (position);
		}
		return magneticField;
	}
}
