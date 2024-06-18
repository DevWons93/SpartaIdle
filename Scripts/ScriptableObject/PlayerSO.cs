using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Character/Player")]
public class PlayerSO : ScriptableObject
{
    [field: SerializeField] public int Damage;    
    [field: SerializeField] public float AttackRange { get; private set; } = 1.5f;
    [field: SerializeField] public float RunSpeed { get; private set; } = 5f;
    [field: SerializeField] public float AttackInterval { get; private set; }
}
