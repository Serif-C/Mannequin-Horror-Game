using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostMovement : MonoBehaviour
{
    /* **** Script Summary ****
     * Controls the "non-damaging" Mannequins to slowly creep-up 
     * to the player whenever they are out of sight
     */

    [Header("References")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform player;
    [SerializeField] private GhostBehaviour behaviour;

    [Header("General Attributes")]
    [SerializeField] private bool isInLineOfSight = false;

    private void Start()
    {
        // A bit of error handling, 
        if(player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if(playerObject != null)
            {
                player = playerObject.transform;
            }
            else
            {
                Debug.LogError("Player GameObject not found!");
            }
        }

        // Ensure GhostBehaviour is assigned
        if (behaviour == null)
        {
            behaviour = GetComponent<GhostBehaviour>();
            if (behaviour == null)
            {
                Debug.LogError("GhostBehaviour component not found on this GameObject!");
            }
        }

        // Ensure the agent component exists
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
            if (agent == null)
            {
                Debug.LogError("NavMeshAgent component not found on this GameObject!");
            }
        }
    }

    private void LateUpdate()
    {
        if (player == null || behaviour == null || agent == null)
        { 
            return; // Do nothing
        }

        // Continuously check if the ghost is in line of sight and adjust movement accordingly
        if (!isInLineOfSight)
        {
            MoveTowardsPlayer();
        }
        else
        {
            StopMoving();
        }
    }

    private void MoveTowardsPlayer()
    {
        // Update player reference every frame (ensures movement remains dynamic)
        player = GameObject.FindGameObjectWithTag("Player").transform;
        int walkLevel = behaviour.GetWalkLevel();

        // Varied movement for testing purposes only
        switch (walkLevel)
        {
            case 0:
                agent.speed = 0f; // No movement
                break;

            case 1:
                agent.speed = 1.5f;
                agent.stoppingDistance = 5f;
                break;

            case 2:
                agent.speed = 2.5f;
                agent.stoppingDistance = 4f;
                break;

            case 3:
                agent.speed = 3.0f;
                agent.stoppingDistance = 3f;
                break;

            case 4:
                agent.speed = 3.5f;
                agent.stoppingDistance = 1.5f;
                break;
        }

        // Move towards the player
        agent.SetDestination(player.position);
    }

    private void StopMoving()
    {
        // Stop the ghost when it's in the player's line of sight
        agent.speed = 0f;
        agent.ResetPath(); // Stop movement immediately
    }

    public void SetIsInLineOfSight(bool inSight)
    {
        isInLineOfSight = inSight;

        if (inSight)
        {
            StopMoving();
        }
    }

    private void WalkBehaviour()
    {

    }
}
