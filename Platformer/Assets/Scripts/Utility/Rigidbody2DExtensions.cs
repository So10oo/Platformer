using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public static class Rigidbody2DExtensions
{
    public static void SetVelocityX(this Rigidbody2D rb, float value)
    {
        rb.velocity = rb.velocity.SetX(value);
    }

    public static void SetVelocityY(this Rigidbody2D rb, float value)
    {
        rb.velocity = rb.velocity.SetY(value);
    }

}

