using System;
using UnityEngine;

public class RotateView : IView<float>
{
    public Action<Vector3> Rotate;

    Transform _transform;

    readonly Vector3 _right;
    readonly Vector3 _left;

    RotateMode _mode;
    public RotateView(Transform transform, RotateMode rotateMode = RotateMode.X)
    {
        _mode = rotateMode;
        var temp = transform.localScale;
        _right = temp;
        switch (rotateMode)
        {
            case RotateMode.X:
                temp.x = -temp.x;
                break;
            case RotateMode.Y:
                temp.y = -temp.y;
                break;
            case RotateMode.Z:
                temp.z = -temp.z;
                break;
        }
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

    public float GetRotate()
    {
        float dir = default;
        switch (_mode)
        {
            case RotateMode.X:
                dir = _transform.localScale.x;
                break;
            case RotateMode.Y:
                dir = _transform.localScale.y;
                break;
            case RotateMode.Z:
                dir = _transform.localScale.z;
                break;
        }
        return dir;
    }

    public enum RotateMode
    {
        X,
        Y,
        Z
    };

}

