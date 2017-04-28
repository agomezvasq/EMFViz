using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EllipticIntegralTable {

    private Dictionary<double, Tuple> _values;

    public EllipticIntegralTable(string eFilename)
    {
        _values = new Dictionary<double, Tuple>(100001);

        StreamReader streamReader = new StreamReader(eFilename);

        for (int i = 0; i < 100001; i++)
        {
            double x = (double)i / 100000D;

            string line = streamReader.ReadLine();

            string[] words = line.Split();

            double first = 0D;
            if (!words[0].Equals("Inf"))
            {
                first = double.Parse(words[0]);
            }
            else
            {
                first = double.MaxValue;
            }
            double second = double.Parse(words[1]);

            _values[x] = new Tuple(first, second);
        }

        streamReader.Close();
    }

    public double GetEI(double x)
    {
        x = Math.Round(x, 5);
        Tuple tuple = _values[x];
        return tuple.First;
    }

    public double GetEII(double x)
    {
        x = Math.Round(x, 5);
        Tuple tuple = _values[x];
        return tuple.Second;
    }
}