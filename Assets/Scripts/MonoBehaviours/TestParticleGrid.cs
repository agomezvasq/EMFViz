using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestParticleGrid : MonoBehaviour {

    public double resolution;
    public Field field;
    public TestParticle gameObj;
    public Vector3 initialPosition;
    public Vector3 initialVelocity;

    private FieldSuperpositioner fieldSuperpositioner;
    private ObjGrid<TestParticle> objGrid;
    private int interval;

	// Use this for initialization
	void Start () {
        switch (field) {
            case Field.Electric:
                fieldSuperpositioner = new ElectricFieldSuperpositioner();
                break;
            case Field.Magnetic:
                fieldSuperpositioner = new MagneticFieldSuperpositioner();
                break;
            default:
                fieldSuperpositioner = null;
                break;
        }

        FieldGenerator[] fieldGenerators = FindObjectsOfType<FieldGenerator>();

        foreach (FieldGenerator fieldGenerator in fieldGenerators)
        {
            fieldSuperpositioner.Add(fieldGenerator);
        }

        Vector3 scale = (float)resolution * Vector3.one;
		Vector2 ssSize = Camera.main.WorldToScreenPoint (scale - Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0)));
 
        int rows = (int)(Screen.height / ssSize.y) + 1;
        int columns = (int)(Screen.width / ssSize.x) + 1;

        objGrid = new ObjGrid<TestParticle>(rows, columns, rows);

        interval = Mathf.RoundToInt((float)(1D / resolution));

		for (int i = 0; i < objGrid.Rows(); i += interval) {
			for (int j = 0; j < objGrid.Columns(); j += interval) {
				for (int k = 0; k < objGrid.Aisles(); k += interval) {
                    Vector3 ssPosition = new Vector3(ssSize.x * (j + 0.5f), ssSize.y * (i + 0.5f), 0f);
                    Vector3 position = Camera.main.ScreenToWorldPoint(ssPosition);
                    
					position = initialPosition + (new Vector3 (position.x, position.y, (k / interval + 0.5f) - 10.5f - 9.5f / 2f));

                    TestParticle instantiatedGameObject = Instantiate (gameObj, position, transform.rotation) as TestParticle;
					instantiatedGameObject.transform.localScale = scale;
					instantiatedGameObject.transform.SetParent (transform);
                    TestParticle testParticle = instantiatedGameObject.GetComponent<TestParticle>();
                    testParticle.velocity = initialVelocity;
                    objGrid.Set(testParticle, i, j, k);
                }
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
        UpdateField();
	}

    public void UpdateField() {
        for (int i = 0; i < objGrid.Rows(); i += interval) {
            for (int j = 0; j < objGrid.Columns(); j += interval) {
                for (int k = 0; k < objGrid.Aisles(); k += interval) {
                    TestParticle obj = objGrid.Get(i, j, k);

                    Vector3 position = obj.transform.position;
                    Vector3 initialPosition = obj.GetInitialPosition();
                    
                    Vector3 field = fieldSuperpositioner.Field(position);

                    obj.velocity = field;
                    obj.GetTrailRenderer().time = 5 * (position - initialPosition).magnitude;
                }
            }
        }
    }
}

public enum Field
{
    Electric,
    Magnetic
}