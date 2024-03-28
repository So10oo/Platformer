using UnityEngine;

public class FpsLock : MonoBehaviour
{
    [SerializeField] int _fps = 60;

    private void OnValidate()
    {
        Application.targetFrameRate = _fps;
    }
}
