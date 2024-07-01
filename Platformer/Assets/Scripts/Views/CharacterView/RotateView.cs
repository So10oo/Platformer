using System;
using UnityEngine;

public class RotateView : IView<float>
{
    public Action<Vector2> Rotate;

    Transform _transform;

    readonly Vector2 _right;
    readonly Vector2 _left;

    public RotateView(Transform transform)
    {
        var temp = transform.localScale;
        _right = temp;
        temp.x = -temp.x;
        _left = temp;
        _transform = transform;
    }

    public void ViewData(float data)
    {
        if (data > 0)
            _transform.localScale = _right;
        else if (data < 0)
            _transform.localScale = _left;

        Rotate?.Invoke(_transform.localScale);
    }


}

