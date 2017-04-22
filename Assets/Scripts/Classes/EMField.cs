using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EMField {

	public static double PERMITTIVITY = 1D;
	public static double K = 1D / (4D * Mathf.PI * PERMITTIVITY);

	public static double PERMEABILITY = 1D;
	public static double H = PERMEABILITY / (4D * Mathf.PI);


	private static double [] EI_TERMS = {
		1.00000000D, 0.25000000D, 0.14062500D, 0.09765625D, 0.07476807D, 0.06056213D, 
		0.05088902D, 0.04387879D, 0.03856535D, 0.03439934D, 0.03104540D, 0.02828724D,
		0.02597908D, 0.02401912D, 0.02233410D, 0.02086998D, 0.01958598D, 0.01845081D,
		0.01744000D, 0.01653419D, 0.01571781D, 0.01497825D, 0.01430516D, 0.01368996D,
		0.01312548D, 0.01260572D, 0.01212554D, 0.01168060D
	};

	private static double [] EII_TERMS = {
		0.2500000000D, 0.0468750000D, 0.0195312500D, 0.0106811500D, 0.0067291260D, 0.0046262740D,
		0.0033752920D, 0.0025710230D, 0.0020234900D, 0.0016339680D, 0.0013470110D, 0.0011295250D,
		0.0009607646D, 0.0008271890D, 0.0007196544D, 0.0006318059D, 0.0005591154D, 0.0004982858D,
		0.0004468699D, 0.0004030207D, 0.0003653232D, 0.0003326781D, 0.0003042213D, 0.0002792656D,
		0.0002572595D, 0.0002377557D, 0.0002203888D
	};

	public static double EllipticIntegralI (float x) {
		return EllipticIntegralI (x, EI_TERMS.Length);
	}

	public static double EllipticIntegralII (float x) {
		return EllipticIntegralII (x, EII_TERMS.Length);
	}

	public static double EllipticIntegralI (float x, int nTerms) {
		double ei = 0f;
		for (int i = 0; i < nTerms; i++) {
			ei += Mathf.Pow (x, i * 2) * EI_TERMS [i]; 
		}
		ei *= Mathf.PI / 2D;
		return ei;
	}

	public static double EllipticIntegralII(float x, int nTerms) {
		double eii = 0f;
		for (int i = 0; i < nTerms; i++) {
			eii += Mathf.Pow (x, (i + 1) * 2) * EII_TERMS [i]; 
		}
		eii = Mathf.PI / 2D * (1D - eii);
		return eii;
	}
}
