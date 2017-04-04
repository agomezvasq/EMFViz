using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestParticleField : MonoBehaviour {

	public TestParticle gameObj;
	public Vector3 scale;

	public Vector3[,,] initialPositions;
	private TestParticle[,,] testParticles;

	// Use this for initialization
	void Start () {
		Vector2 ssSize = Camera.main.WorldToScreenPoint (scale - Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0)));
		testParticles = new TestParticle [(int)(Screen.height / ssSize.y) + 1, (int)(Screen.width / ssSize.x) + 1, (int)(Screen.height / ssSize.y) + 1];
		initialPositions = new Vector3 [Rows (), Columns (), Aisles ()];
		int c = 16;
		for (int i = 0; i < Rows (); i+=c) {
			for (int j = 0; j < Columns (); j+=c) {
				for (int k = 0; k < Aisles (); k+=c) {
					Vector3 position = Camera.main.ScreenToWorldPoint (new Vector3 (ssSize.x * (j + 0.5f), ssSize.y * (i + 0.5f), 0.0f));
					position = new Vector3 (position.x, position.y, (k / c + 0.5f) - 9.5f - 9.5f / 2.0f - 0.5f);
					initialPositions [i, j, k] = position;

					GameObject instantiatedGameObject = GameObject.Instantiate (gameObj.gameObject, position, transform.rotation) as GameObject;
					instantiatedGameObject.transform.localScale = scale;
					instantiatedGameObject.transform.SetParent (transform);
					testParticles [i, j, k] = instantiatedGameObject.GetComponent<TestParticle> ();
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public TestParticle Get (int row, int column, int aisle) {
		return testParticles [row, column, aisle];
	}

	public int Rows () {
		return testParticles.GetLength (0);
	}

	public int Columns () {
		return testParticles.GetLength (1);
	}

	public int Aisles () {
		return testParticles.GetLength (2);
	}
}
