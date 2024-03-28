using UnityEngine;

public class RotateView : IView<float>
{
    Transform _transform;

    readonly Vector3 _right;
    readonly Vector3 _left;

    public RotateView(Transform transform)
    { 
        var temp = transform.localScale;
        _right = temp;
        temp.x = -temp.x;
        _left = temp;

        _transform = transform;
    }

    float _previousData;
    public void ViewData(float data)
    {
        if (_previousData == data) return;
        else _previousData = data;

        if (data > 0)
            _transform.localScale = _right;
        else if (data < 0)
            _transform.localScale = _left;
    }
}

