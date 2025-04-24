using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Object/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private float spped = 1.0f;

    public float Spped { get { return spped; } }

    [SerializeField] private float attack = 10.0f;

    public float Attack { get { return attack; } }

    [SerializeField] private bool isXDirPositive = false;

    public bool IsXDirPositive { get { return isXDirPositive; } }
}