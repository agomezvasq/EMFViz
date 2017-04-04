using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

	//public GameObject arrowPrefab;
	public Sphere sphere1;
	public Sphere sphere2;
	public ParticleList particleList;
	//private GameObjectGrid arrowPrefabGrid;
	public TestParticleField testParticleField;

	// Use this for initialization
	void Start () {
		//arrowPrefabGrid = new GameObjectGrid (arrowPrefab, 0.3f * Vector3.one, gameObject);
		particleList = new ParticleList ();
		particleList.Add (sphere1);
		particleList.Add (sphere2);
	}

	// Update is called once per frame
	void Update () {
		/*for (int i = 0; i < arrowPrefabGrid.Rows(); i+=3) {
			for (int j = 0; j < arrowPrefabGrid.Columns(); j+=3) {
				Vector2 arrowPrefabToSphere1 = arrowPrefabGrid.Get (i, j).transform.position - sphere1.transform.position;
				Vector2 arrowPrefabToSphere2 = arrowPrefabGrid.Get (i	, j).transform.position - sphere2.transform.position;
				arrowPrefabToSphere1 = arrowPrefabToSphere1.normalized * 1.0f / arrowPrefabToSphere1.SqrMagnitude();
				arrowPrefabToSphere2 = arrowPrefabToSphere2.normalized * -1.0f / arrowPrefabToSphere2.SqrMagnitude();
				Vector2 vector2 = arrowPrefabToSphere1 + arrowPrefabToSphere2;
				arrowPrefabGrid.Get(i, j).transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Atan2 (vector2.y, vector2.x) * 180.0f / Mathf.PI - 90.0f);
			}
		}*/
		int c = 0;
		for (int i = 0; i < testParticleField.Rows(); i+=16) {
			for (int j = 0; j < testParticleField.Columns(); j+=16) {
				for (int k = 0; k < testParticleField.Aisles (); k += 16) {
					if (testParticleField.Get (i, j, k) == null) {
						c++;
						continue;
					}
					//if (circlePrefabToSphere1.magnitude >= 0.5f - 0.0625f && circlePrefabToSphere2.magnitude >= 0.5f - 0.0625f) {
					Vector3 position = testParticleField.Get (i, j, k).transform.position;
					//Vector3 emField = particleList.ElectricField (position) + particleList.MagneticField (position);

					//if ((circlePrefabGrid.positions[i, j] + vector3 - sphere1.transform.localPosition).magnitude >= 1) {
					//circlePrefabGrid.Get (i, j).GetComponent<TestParticle> ().Acceleration(vector3);
					//testParticleField.Get (i, j, k).transform.position = testParticleField.initialPositions [i, j, k] + particleList.ElectricField (testParticleField.initialPositions [i, j, k]);
					Vector3 initialPosition = testParticleField.initialPositions [i, j, k];
					//if (!((initialPosition - sphere1.transform.position).magnitude <= 0.5f || (initialPosition - sphere2.transform.position).magnitude <= 0.5f)) {
						testParticleField.Get (i, j, k).transform.position = initialPosition + particleList.ElectricField (initialPosition);
					//} 
					//else {
					//	testParticleField.Get (i, j, k).GetComponent<MeshRenderer> ().enabled = false;
					//	DestroyImmediate (testParticleField.Get (i, j, k));
					//}
				}
			}
		}
		//print (c);
	}
}
