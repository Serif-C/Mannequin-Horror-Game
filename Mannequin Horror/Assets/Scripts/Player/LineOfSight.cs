using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask collisionLayer;  // Walls and stuff

    [Header("Attributes")]
    [SerializeField] private float fieldOfView = 90f;   // FOV in degrees
    [SerializeField] private float viewDistance = 10f;

    private void Update()
    {
        DetectMannequinsInSight();
    }

    private bool IsEnemyInSight(Transform enemy)
    {
        Vector3 directionToEnemy = (enemy.position - transform.position).normalized;
        float distanceFromEnemy = Vector3.Distance(transform.position, enemy.position);

        // Check if within field of view
        if (Vector3.Angle(transform.forward, directionToEnemy) > fieldOfView / 2)
        {
            return false;
        }

        // Check if there is an obstruction
        if (Physics.Raycast(transform.position, directionToEnemy, out RaycastHit hit, distanceFromEnemy, collisionLayer))
        {
            return false;
        }

        return true;
    }

    private void DetectMannequinsInSight()
    {
        Collider[] mannequinsInRange = Physics.OverlapSphere(transform.position, viewDistance, enemyLayer);

        foreach (Collider manneqiun in mannequinsInRange)
        {
            if(IsEnemyInSight(manneqiun.transform))
            {
                Debug.Log("Seeing a mannequin of name: " + manneqiun.gameObject.name);
            }
        }
    }
}
