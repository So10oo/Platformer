using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExtensions 
{
    public static Vector2 SetX(this Vector2 vector, float x)
    {
        vector.x = x;
        return vector;
    }

    public static Vector2 SetY(this Vector2 vector, float y)
    {
        vector.y = y;
        return vector;
    }
}
