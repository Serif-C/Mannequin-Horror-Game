using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GhostBehaviour : MonoBehaviour
{
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
    private int m_WalkLevel = 0;
    private int m_PossessionLevel = 0;
    private int m_ContorsionLevel = 0;
    private int m_LevitateLevel = 0;

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

        // Reference the behaviours for each intensity through `Demons` script and assign levels for each action
        switch (intensity)
        {
            case BehaviourIntensity.VERY_LOW:
                demonThisRound.VeryLowBehaviourLevels(ref m_WalkLevel, ref m_PossessionLevel, ref m_ContorsionLevel, ref m_LevitateLevel);
                break;

            case BehaviourIntensity.LOW:
                demonThisRound.LowBehaviourLevels(ref m_WalkLevel, ref m_PossessionLevel, ref m_ContorsionLevel, ref m_LevitateLevel);
                break;

            case BehaviourIntensity.MEDIUM:
                demonThisRound.MediumBehaviourLevels(ref m_WalkLevel, ref m_PossessionLevel, ref m_ContorsionLevel, ref m_LevitateLevel);
                break;

            case BehaviourIntensity.HIGH:
                demonThisRound.HighBehaviourLevels(ref m_WalkLevel, ref m_PossessionLevel, ref m_ContorsionLevel, ref m_LevitateLevel);
                break;

            case BehaviourIntensity.VERY_HIGH:
                demonThisRound.VeryHighBehaviourLevels(ref m_WalkLevel, ref m_PossessionLevel, ref m_ContorsionLevel, ref m_LevitateLevel);
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

    // GETTERS and SETTERS //

    public BehaviourIntensity GetIntensity()
    {
        return intensity;
    }

    
    public int GetWalkLevel() // Movement speed is based off this level
    {
        return m_WalkLevel;
    }

    public int GetPossessionLevel()
    {
        return m_PossessionLevel;
    }

    public int GetContorsionLevel()
    {
        return m_ContorsionLevel;
    }

    public int GetLevitationLevel()
    {
        return m_LevitateLevel;
    }
}
