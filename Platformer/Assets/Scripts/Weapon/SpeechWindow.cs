using TMPro;
using UnityEngine;


public class SpeechWindow : MonoBehaviour
{
    [SerializeField] TextMeshPro _textMeshPro;

    public string text
    {
        get 
        { 
            return _textMeshPro.text; 
        }
        set 
        { 
            _textMeshPro.text = value; 
        }
    }

    public void SetRotateView(RotateView rotateView)
    {
        rotateView.Rotate += (v) =>
        {
            _textMeshPro.gameObject.transform.localScale = new Vector2(v.x, v.y);
        };
    }
}

