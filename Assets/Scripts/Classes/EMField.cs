using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EMField {

	public static double PERMITTIVITY = 1D;
	public static double K = 1D / (4D * Mathf.PI * PERMITTIVITY);

	public static double PERMEABILITY = 1D;
	public static double H = PERMEABILITY / (4D * Mathf.PI);
}
