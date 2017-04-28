using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FieldSuperpositioner {

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

    public abstract Vector3 Field(Vector3 position);
}
