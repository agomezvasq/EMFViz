using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Ring : FieldGenerator {

	public float radius = 0.5f;
	public float tubeRadius = 0.03125f;

    public double angularSpeed;
    public Vector3 normal;

    private int numSegments = 32;
	private int numTubes = 12;

    protected override void Start() {
        GetComponent<MeshFilter>().mesh = RingCreator.CreateRing(radius, tubeRadius, numSegments, numTubes);

        lastPos = transform.position;

        transform.GetComponent<MeshRenderer>().material.color = GetColorFromCharge(charge);

        float internalRadius = radius - tubeRadius * 2f;
        
        ChargedObject = new ChargedRing(charge, transform.position, transform.rotation * Vector3.up, radius, internalRadius, angularSpeed);
    }

    // Update is called once per frame
    protected override void Update()
    {
        transform.position += ChargedObject.Velocity * Time.deltaTime;

        if (charge != ChargedObject.Charge)
        {
            GetComponent<MeshRenderer>().material.color = GetColorFromCharge(charge);
            ChargedObject.Charge = charge;
        }
        if (angularSpeed != ((ChargedRing)ChargedObject).AngularSpeed)
        {
            ((ChargedRing)ChargedObject).AngularSpeed = angularSpeed;
        }

        float angle = (float)((ChargedRing)ChargedObject).AngularSpeed * Time.deltaTime;
        transform.Rotate(((ChargedRing)ChargedObject).Normal, angle, Space.World);
    }
}
