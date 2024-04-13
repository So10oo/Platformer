using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "PatrollerSettings", menuName = "ScriptableObjects/PatrollerSettings")]
public class PatrollerSettings : ScriptableObject
{
    [Header("PatrollingStatus")]
    [SerializeField] float _rangeVision;
    [SerializeField] float _speed;
    [SerializeField] float _patrollerRadios;
    [SerializeField] LayerMask _detectionMask;
    public float rangeVision => _rangeVision;
    public float speed => _speed;
    public float patrollerRadios => _patrollerRadios;
    public LayerMask detectionMask => _detectionMask;
}

