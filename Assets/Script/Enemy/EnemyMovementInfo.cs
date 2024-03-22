using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementInfo : MonoBehaviour
{
    public float maxSlopeAngle;
    public Vector2 slopeNormalPerp;
    public float slopeDownAngle;
    public float lastSlopeAngle;
    public float slopeSideAngle;
    public float xInput = 1;
    public bool isCollidingWithPlatform;
    public bool canWalkOnSlope = false;
}
