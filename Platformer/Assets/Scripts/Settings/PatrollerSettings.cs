using UnityEngine;

[CreateAssetMenu(fileName = "PatrollerSettings", menuName = "ScriptableObjects/PatrollerSettings")]
public class PatrollerSettings : ScriptableObject
{
    [Header("PatrollingStatus")]
    [SerializeField] float _rangeVision;
    [SerializeField] float _patrollingSpeed;
    [SerializeField] float _patrollerRadios;
    [SerializeField] LayerMask _detectionMask;
    public float rangeVision => _rangeVision;
    public float patrollingSpeed => _patrollingSpeed;
    public float patrollerRadios => _patrollerRadios;
    public LayerMask detectionMask => _detectionMask;

    [Header("PursuingStatus")]
    [SerializeField] float _pursuingSpeed;
    public float pursuingSpeed => _pursuingSpeed;


    [Header("FailureStatus")]
    [SerializeField] float _timeFailureStatus;
    public float timeFailureStatus => _timeFailureStatus;

    [Header("DetectingStatue")]
    [SerializeField] float _timeDetectingStatue;
    public float timeDetectingStatue => _timeDetectingStatue;

}

