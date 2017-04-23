﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjGrid<T> {

    private T[,,] ts;
    private Vector3[,,] initialPositions;

    public ObjGrid(int rows, int columns, int aisles)
    {
        ts = new T[rows, columns, aisles];
    }

    public T Get(int row, int column, int aisle)
    {
        return ts[row, column, aisle];
    }

    public void Set(T t, int row, int column, int aisle)
    {
        ts[row, column, aisle] = t;
    }

    public int Rows()
    {
        return ts.GetLength(0);
    }

    public int Columns()
    {
        return ts.GetLength(1);
    }

    public int Aisles()
    {
        return ts.GetLength(2);
    }

    public Vector3 GetInitialPosition(int row, int column, int aisle)
    {
        return initialPositions[row, column, aisle];
    }

    public void SetInitialPosition(Vector3 initialPosition, int row, int column, int aisle)
    {
        initialPositions[row, column, aisle] = initialPosition;
    }
}
