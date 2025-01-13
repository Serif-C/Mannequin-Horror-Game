using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private Demons demonThisRound;

    [Header("Enemy Status")]
    [SerializeField] private bool isPossessed = false;
    [SerializeField] private float hauntChance = 0.002f;  // 0.2% chance that a demon would start haunting (calculated every second)
    [SerializeField] private bool isHaunting = false;
    [SerializeField] private float hauntDuration = 10f;
    [SerializeField] private BehaviourIntensity intensity;

    public enum BehaviourIntensity
    {
        VERY_LOW,
        LOW,
        MEDIUM,
        HIGH,
        VERY_HIGH
    }

    private void Start()
    {
        demonThisRound = FindAnyObjectByType<Demons>();
        StartCoroutine(CalculateHauntChance());
    }

    private void Update()
    {
        AssignBehaviourState();

        Debug.Log("playerSanity: " + playerSanity);

        // Reference the behaviours for each intensity through `Demons` script
        switch (intensity)
        {
            case BehaviourIntensity.VERY_LOW:
                demonThisRound.VeryLowBehaviour();
                break;

            case BehaviourIntensity.LOW:
                demonThisRound.LowBehaviour();
                break;

            case BehaviourIntensity.MEDIUM:
                demonThisRound.MediumBehaviour();
                break;

            case BehaviourIntensity.HIGH:
                demonThisRound.HighBehaviour();
                break;

            case BehaviourIntensity.VERY_HIGH:
                demonThisRound.VeryHighBehaviour();
                break;
        }
    }

    private void AssignBehaviourState()
    {
        playerSanity = FindAnyObjectByType<SanityManager>().GetSanityValue();

        if (playerSanity >= 80f && playerSanity <= 100f)
            intensity = BehaviourIntensity.VERY_LOW;

        else if (playerSanity >= 60f && playerSanity < 80f)
            intensity = BehaviourIntensity.LOW;

        else if (playerSanity >= 40f && playerSanity < 60f)
            intensity = BehaviourIntensity.MEDIUM;

        else if (playerSanity >= 20f && playerSanity < 40f)
            intensity = BehaviourIntensity.HIGH;

        else if (playerSanity >= 0f && playerSanity < 20f)
            intensity = BehaviourIntensity.VERY_HIGH;
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

    public BehaviourIntensity GetIntensity()
    {
        return intensity;
    }
}
