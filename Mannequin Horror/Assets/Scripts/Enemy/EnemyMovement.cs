using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private NavMeshAgent enemy;
    [SerializeField] private Transform player;
}
