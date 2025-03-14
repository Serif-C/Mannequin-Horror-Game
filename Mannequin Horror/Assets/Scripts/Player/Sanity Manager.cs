using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityManager : MonoBehaviour
{
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
