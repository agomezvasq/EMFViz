using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestParticleGrid : MonoBehaviour {
    
    public double resolution;
    public Vector3 scale;

    public TestParticle gameObj;

    public ObjGrid<TestParticle> objGrid;

    private int interval;

	// Use this for initialization
	void Start () {
		Vector2 ssSize = Camera.main.WorldToScreenPoint (scale - Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0)));
 
        int rows = (int)(Screen.height / ssSize.y) + 1;
        int columns = (int)(Screen.width / ssSize.x) + 1;

        objGrid = new ObjGrid<TestParticle>(rows, columns, rows);

        interval = Mathf.RoundToInt((float)(1D / resolution));

        if (interval == 0)
        {
            return;
        }

		for (int i = 0; i < objGrid.Rows(); i += interval) {
			for (int j = 0; j < objGrid.Columns(); j += interval) {
				for (int k = 0; k < objGrid.Aisles(); k += interval) {
                    Vector3 ssPosition = new Vector3(ssSize.x * (j + 0.5f), ssSize.y * (i + 0.5f), 0f);
                    Vector3 position = Camera.main.ScreenToWorldPoint(ssPosition);

					position = new Vector3 (position.x, position.y, ssSize.x * (k + 0.5f) - 10f - 9.5f / 2f);
					objGrid.SetInitialPosition(position, i, j, k);

                    TestParticle instantiatedGameObject = Instantiate (gameObj, position, transform.rotation) as TestParticle;
					instantiatedGameObject.transform.localScale = scale;
					instantiatedGameObject.transform.SetParent (transform);
                    TestParticle testParticle = instantiatedGameObject.GetComponent<TestParticle>();
                    objGrid.Set(testParticle, i, j, k);
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateFields(FieldSuperpositioner fieldSuperpositioner)
    {
        if (interval == 0)
        {
            return;
        }

        for (int i = 0; i < objGrid.Rows(); i += interval) {
            for (int j = 0; j < objGrid.Columns(); j += interval) {
                for (int k = 0; k < objGrid.Aisles(); k += interval) {
                    Vector3 position = objGrid.Get(i, j, k).transform.position;

                    Vector3 initialPosition = objGrid.GetInitialPosition(i, j, k);
                    Vector3 magneticField = fieldSuperpositioner.MagneticField(position);

                    objGrid.Get(i, j, k).velocity = magneticField;
                }
            }
        }
    }
}
