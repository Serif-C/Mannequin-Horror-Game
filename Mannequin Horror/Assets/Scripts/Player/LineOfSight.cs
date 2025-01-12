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

    private List<GhostMovement> allEnemies = new List<GhostMovement>();

    private void Start()
    {
        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            GhostMovement ghostMovement = enemy.GetComponent<GhostMovement>();
            if (ghostMovement != null)
            {
                allEnemies.Add(ghostMovement);
            }
        }
    }

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
            //enemy.GetComponent<GhostMovement>().SetIsInLineOfSight(false);
            return false;
        }

        // Check if there is an obstruction
        if (Physics.Raycast(transform.position, directionToEnemy, out RaycastHit hit, distanceFromEnemy, collisionLayer))
        {
            //Debug.Log("Raycast hit: " + hit.collider.name);
            //enemy.GetComponent<GhostMovement>().SetIsInLineOfSight(false);
            return false;
        }

        // When an enemy is in Line of Sight toggle its attribute 'isInLineOfSight' to true
        //enemy.GetComponent<GhostMovement>().SetIsInLineOfSight(true);
        return true;
    }

    private void DetectMannequinsInSight()
    {
        Collider[] mannequinsInRange = Physics.OverlapSphere(transform.position, viewDistance, enemyLayer);
        HashSet<GhostMovement> enemiesInRange = new HashSet<GhostMovement>();

        // Process enemies inside the detection radius
        foreach(Collider mannequin in mannequinsInRange)
        {
            GhostMovement ghostMovement = mannequin.GetComponent<GhostMovement>();

            if(ghostMovement != null)
            {
                bool isSeen = IsEnemyInSight(mannequin.transform);
                ghostMovement.SetIsInLineOfSight(isSeen);
                enemiesInRange.Add(ghostMovement);
            }
        }

        // Ensure enemies outside the sphere still move towards the player
        foreach(GhostMovement enemy in allEnemies)
        {
            if(!enemiesInRange.Contains(enemy))
            {
                enemy.SetIsInLineOfSight(false);
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Draw vision sphere
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewDistance);

        // Draw vision cone
        Vector3 leftBoundary = Quaternion.Euler(0, -fieldOfView / 2, 0) * transform.forward * viewDistance;
        Vector3 rightBoundary = Quaternion.Euler(0, fieldOfView / 2, 0) * transform.forward * viewDistance;

        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, leftBoundary);
        Gizmos.DrawRay(transform.position, rightBoundary);
    }
}
