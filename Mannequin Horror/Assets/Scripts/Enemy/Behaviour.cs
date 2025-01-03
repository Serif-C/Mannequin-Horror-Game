using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour : MonoBehaviour
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
    [SerializeField] private GameObject playerSanity;

    [Header("Enemy Status")]
    [SerializeField] private BehaviourIntensity intensity;
    [SerializeField] private bool isPossessed = false;
    [SerializeField] private bool isHaunting = false;

    private enum BehaviourIntensity
    {
        VERY_LOW,
        LOW,
        MEDUIM,
        HIGH,
        VERY_HIGH
    }
}
