using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Player : MonoBehaviour
{
    public static bool isAlive = true;
    public static bool isFacingRight = true;
    public static bool isJumping;
    public static bool isGrounded;
    public static bool isGrabbingLedge;
    public static bool isClimbing;
    public static bool isOnSlope;
    public static bool isHolding;
    public static bool isPlaying;
    public static int charge = 0;
}
