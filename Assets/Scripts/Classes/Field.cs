using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field {

	public Field () {

	}

	public virtual Vector3 Get (Vector3 position) {
		return new Vector3 (position.x, position.y, position.z);
	}
}
