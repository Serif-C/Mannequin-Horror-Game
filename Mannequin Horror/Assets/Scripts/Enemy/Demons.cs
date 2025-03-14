using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demons : MonoBehaviour
{
    /*
     * - There should only be a single instance of this Script per Round/Level
     * - 
     */

    [Header("Default Demon Obvious Clues")]
    [SerializeField] private bool canWalk = false;
    [SerializeField] private bool canBePossessed = false;
    [SerializeField] private bool canContorque = false;
    [SerializeField] private bool canLevitate = false;
    [SerializeField] private int[] intensityLevel = { 0, 1, 2, 3, 4, 5}; // Controls frequency and intensity

    // These clues are conditional and can be missed if not checked properly
    [Header("Default Demon Hidden Clues")]
    [SerializeField] private bool canLeaveHandprints = false;
    [SerializeField] private bool canJumpscare = false;
    [SerializeField] private bool canCauseBugInfestation = false;

    [Header("References")]
    [SerializeField] private List<GhostBehaviour> enemiesToPossess = new List<GhostBehaviour>();

    private DemonType demonType;

    // Add a couple more later
    public enum DemonType
    {
        DEFAULT
    }

    private void Start()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            GhostBehaviour ghostBehaviour = enemy.GetComponent<GhostBehaviour>();
            if (ghostBehaviour != null )
            {
                enemiesToPossess.Add(ghostBehaviour);
            }
        }
    }

    // Called by GhostBehaviour script
    public void PossessRandomMannequin()
    {
        int rand = Random.Range(0, enemiesToPossess.Count - 1);
        enemiesToPossess[rand].SetPossession(true);
    }


    public void VeryLowBehaviourLevels(ref int walkLevel, ref int possessionLevel, ref int contorsionLevel, ref int levitateLevel)
    {
        if(demonType == DemonType.DEFAULT)
        {
            canWalk = false;
            canBePossessed = false;
            canContorque = true;
            canLevitate = false;
            walkLevel = intensityLevel[0];
            possessionLevel = intensityLevel[0];
            contorsionLevel = intensityLevel[1];
            levitateLevel = intensityLevel[0];
        }
    }

    public void LowBehaviourLevels(ref int walkLevel, ref int possessionLevel, ref int contorsionLevel, ref int levitateLevel)
    {
        if (demonType == DemonType.DEFAULT)
        {
            canWalk = true;
            canBePossessed = true;
            canContorque = true;
            canLevitate = false;
            walkLevel = intensityLevel[1];
            possessionLevel = intensityLevel[1];
            contorsionLevel = intensityLevel[2];
            levitateLevel = intensityLevel[0];
        }
    }

    public void MediumBehaviourLevels(ref int walkLevel, ref int possessionLevel, ref int contorsionLevel, ref int levitateLevel)
    {
        if (demonType == DemonType.DEFAULT)
        {
            canWalk = true;
            canBePossessed = true;
            canContorque = true;
            canLevitate = false;
            walkLevel = intensityLevel[2];
            possessionLevel = intensityLevel[2];
            contorsionLevel = intensityLevel[3];
            levitateLevel = intensityLevel[0];
        }
    }

    public void HighBehaviourLevels(ref int walkLevel, ref int possessionLevel, ref int contorsionLevel, ref int levitateLevel)
    {
        if (demonType == DemonType.DEFAULT)
        {
            canWalk = true;
            canBePossessed = true;
            canContorque = true;
            canLevitate = true;
            walkLevel = intensityLevel[3];
            possessionLevel = intensityLevel[3];
            contorsionLevel = intensityLevel[4];
            levitateLevel = intensityLevel[1];
        }
    }

    public void VeryHighBehaviourLevels(ref int walkLevel, ref int possessionLevel, ref int contorsionLevel, ref int levitateLevel)
    {
        if (demonType == DemonType.DEFAULT)
        {
            canWalk = true;
            canBePossessed = true;
            canContorque = true;
            canLevitate = true;
            walkLevel = intensityLevel[4];
            possessionLevel = intensityLevel[4];
            contorsionLevel = intensityLevel[5];
            levitateLevel = intensityLevel[2];
        }
    }
}
