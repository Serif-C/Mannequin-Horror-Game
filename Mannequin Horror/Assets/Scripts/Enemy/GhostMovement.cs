using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform player;
    [SerializeField] private GhostBehaviour behaviour;

    [Header("General Attributes")]
    [SerializeField] private bool isInLineOfSight = false;

    public void SetIsInLineOfSight(bool inSight)
    {
        isInLineOfSight = inSight;
    }
}
