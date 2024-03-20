using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementInfo : MonoBehaviour
{
    public static float maxSlopeAngle;
    public static Vector2 slopeNormalPerp;
    public static float slopeDownAngle;
    public static float lastSlopeAngle;
    public static float slopeSideAngle;
    public static float xInput;
    public static bool isCollidingWithPlatform;
    public static bool canWalkOnSlope = false;
}
