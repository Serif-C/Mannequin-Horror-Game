using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Possession : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private GhostBehaviour ghostBehaviour;

    private void Update()
    {
        if(ghostBehaviour.GetIsPossessed())
        {
            HauntForThePlayer();
        }
    }

    private void HauntForThePlayer()
    {
        
    }
}
