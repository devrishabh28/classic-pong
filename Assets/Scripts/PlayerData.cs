using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Movement")]
    public float moveSpeed = 10;

    [Space(20)]

    [Header("AI")]
    public bool AI;
}
