using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityManager : MonoBehaviour
{
    /* 
     * Sanity:
     * - Numerical value hidden from player
     * - Responsible for event frequency (Obvious and Hidden Clues)
     * - Gradually decreases with Time
     * - Decreases more rapidly when:
     *      * player is in complete darkness
     *      * under the effect of events (i.e., when eye sight is covered by a mannequin's hands)
     */

    //[Header("References")]

    [Header("Sanity Status")]
    [SerializeField] private float maxSanityValue = 100f;
    [SerializeField] private float currentSanityValue = 0f;
    [SerializeField] private float sanityDecreaseRate = 0.05f;
    [SerializeField] private bool isInDarkness = false;
    [SerializeField] private bool isInfluenced = false;

    private void Start()
    {
        currentSanityValue = maxSanityValue;
    }

    private void Update()
    {
        // Decrease sanity at an even interval
        currentSanityValue -= sanityDecreaseRate * Time.deltaTime;
        Debug.Log("Current Sanity: " + currentSanityValue);
    }

    public float GetSanityValue()
    {
        return currentSanityValue;
    }

    private bool IsInDarkness()
    {
        // Probably use Candles or Flashlight to determine this
        return isInDarkness;
    }

    private bool InInfluenced()
    {
        return isInfluenced;
    }
}
