using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FieldGenerator : MonoBehaviour {

    public double charge;

    protected Vector3 lastPos;

    protected ChargedObject _chargedObject;
    public ChargedObject ChargedObject { get { return _chargedObject; } set { _chargedObject = value; } }

    protected virtual void Start()
    {
        lastPos = gameObject.transform.position;

        GetComponent<MeshRenderer>().material.color = GetColorFromCharge(charge);
    }

    protected virtual void Update()
    {
        transform.position += ChargedObject.Velocity * Time.deltaTime;

        if (charge != ChargedObject.Charge)
        {
            GetComponent<MeshRenderer>().material.color = GetColorFromCharge(ChargedObject.Charge);
            charge = ChargedObject.Charge;
        }
    }

    protected void OnMouseDrag()
    {
        Vector3 screenSpacePosition = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpacePosition.z));
        if (newPosition != transform.position) {
            transform.position = newPosition;
            ChargedObject.Velocity = (transform.position - lastPos) / Time.deltaTime;
            lastPos = transform.position;
        }
        else {
            ChargedObject.Velocity = Vector3.zero;
        }
    }

    private static Color GetColorFromCharge(double charge)
    {
        int compare = charge.CompareTo(0D);
        switch (compare)
        {
            case -1:
                return Color.cyan;
            case 0:
                return Color.white;
            case 1:
                return Color.red;
            default:
                return Color.black;
        }
    }
}
