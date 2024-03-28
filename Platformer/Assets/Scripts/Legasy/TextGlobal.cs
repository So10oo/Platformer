using TMPro;
using UnityEngine;
using Zenject;

public class TextGlobal : MonoBehaviour
{
    public static TextMeshProUGUI info;

    void Awake()
    {
        info = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
