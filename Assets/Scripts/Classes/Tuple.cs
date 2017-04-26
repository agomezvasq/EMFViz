using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuple
{

    private double _first;
    public double First { get { return _first; } set { _first = value; } }

    private double _second;
    public double Second { get { return _second; } set { _second = value; } }

    public Tuple(double first, double second)
    {
        First = first;
        Second = second;
    }

    public override string ToString()
    {
        return "(" + First + ", " + Second + ")";
    }
}