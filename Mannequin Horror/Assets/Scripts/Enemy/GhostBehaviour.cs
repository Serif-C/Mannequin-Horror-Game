using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class GhostBehaviour : MonoBehaviour
{
    /*
     * Enemy Behaviour:
     * - Based off player sanity
     * - More intense and complex behaviour as player sanity diminishes
     * - Example:
     *      * At sanity range 100 to 80:
     *          * Haunting Chance (calculated per second) is very low
     *          * Mannequins can change posture/stance/pose but cannot walk yet
     *      * At sanity range 80 to 60:
     *          * Haunting Chance (calculated per second) is slightly higher
     *          * Mannequins can now walk slowly when outside player line of sight
     *      * Etc...
     */

    [Header("References")]
    [SerializeField] private float playerSanity;
    [SerializeField] private Animator animator;

    [Header("Enemy Status")]
    [SerializeField] private bool isPossessed = false;
    [SerializeField] private float hauntChance = 0.002f;  // 0.2% chance that a demon would start haunting (calculated every second)
    [SerializeField] private bool isHaunting = false;
    [SerializeField] private float hauntDuration = 10f;
    [SerializeField] private bool isInLineOfSight = false;

    [SerializeField] private BehaviourIntensity intensity;
    enum BehaviourIntensity
    {
        VERY_LOW,
        LOW,
        MEDIUM,
        HIGH,
        VERY_HIGH
    }

    private void Start()
    {
        playerSanity = FindAnyObjectByType<SanityManager>().GetSanityValue();
        StartCoroutine(CalculateHauntChance());
    }

    private void Update()
    {
        AssignBehaviourState();

        switch (intensity)
        {
            case BehaviourIntensity.VERY_LOW:
                VeryLowBehaviour();
                break;

            case BehaviourIntensity.LOW:
                LowBehaviour();
                break;

            case BehaviourIntensity.MEDIUM:
                MediumBehaviour();
                break;

            case BehaviourIntensity.HIGH:
                HighBehaviour();
                break;

            case BehaviourIntensity.VERY_HIGH:
                VeryHighBehaviour();
                break;
        }
    }

    private void AssignBehaviourState()
    {
        if (playerSanity >= 80f && playerSanity <= 100f)
            intensity = BehaviourIntensity.VERY_LOW;

        else if (playerSanity >= 60f && playerSanity < 80f)
            intensity = BehaviourIntensity.LOW;

        else if (playerSanity >= 40f && playerSanity < 60f)
            intensity = BehaviourIntensity.MEDIUM;

        else if (playerSanity >= 20f && playerSanity < 40f)
            intensity = BehaviourIntensity.HIGH;

        else
            intensity = BehaviourIntensity.VERY_HIGH;
    }

    private void VeryLowBehaviour()
    {
        /*
         * - Haunting Chance is unchanged from default.
         * - Posture Changes when outside player's line of sight
         */
    }

    private void LowBehaviour()
    {

    }

    private void MediumBehaviour()
    {

    }

    private void HighBehaviour()
    {

    }

    private void VeryHighBehaviour()
    {

    }

    private void StartHaunting()
    {
        // Choose one of the mannequins to chase the player
    }

    private IEnumerator CalculateHauntChance()
    {
        // Only check while not haunting
        while (!isHaunting)
        {
            yield return new WaitForSeconds(1);

            if (Random.value <= hauntChance)
            {
                StartHaunting();
                yield break;    // Stop checking once haunting starts
            }
        }
    }
}
