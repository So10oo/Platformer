using System;
using UnityEngine;

public class Polynomial
{
    float[] p;

    public Polynomial(params float[] p)
    {
        var length = (p.Length % 2 == 1 ? (p.Length + 1) / 2 : p.Length / 2);
        for (int i = 0; i < length; i++)
        {
            (p[i], p[p.Length - i - 1]) = (p[p.Length - i - 1], p[i]);
        }
        this.p = p;
    }

    public float get(float t)
    {
        float sum = 0;
        for (int i = 0; i < p.Length; i++)
        {
            sum += p[i] * Mathf.Pow(t, i);
        }
        return sum;
    }

    public float findRoot(float a, float b, float epsilon)
    {
        int i = 0;
        while (Mathf.Abs(b - a) > epsilon)
        {
            a = a - (b - a) * get(a) / (get(b) - get(a));
            b = b - (a - b) * get(b) / (get(a) - get(b));
            i++;
        }
        return b;
    }
}

