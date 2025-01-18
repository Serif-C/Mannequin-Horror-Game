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
            Debug.Log(gameObject.name + " is Possessed");
            HauntForThePlayer();
        }
    }

    private void HauntForThePlayer()
    {
        
    }
}
