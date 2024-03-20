using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerSlopeHandler : PlayerComponent
{
    [SerializeField]
    private float maxSlopeAngle;
    [SerializeField]
    private float slopeCheckDistance;
    [SerializeField]
    private float collideDistance;
    [SerializeField]
    private float offset;
    [SerializeField]
    private PhysicsMaterial2D fullFriction;
    [SerializeField]
    private PhysicsMaterial2D noFriction;
    private LayerMask groundLayer;
    protected override void Initialization()
    {
        base.Initialization();
        groundLayer = GetComponent<Layers>().GetGround();
        PlayerMovementInfo.maxSlopeAngle = maxSlopeAngle;
    }
    private void Update()
    {
        CheckInsidePlatform();
    }
    private void FixedUpdate()
    {
        SlopeCheck();
    }
    private void CheckInsidePlatform()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, collideDistance, groundLayer);
        PlayerMovementInfo.isCollidingWithPlatform = false;
        if (hit)
        {
            if (hit.collider.CompareTag("Platform"))
            {
                Debug.DrawRay(transform.position, Vector2.down * collideDistance, Color.black);
                PlayerMovementInfo.isCollidingWithPlatform = true;
            }
        }
    }
    private void SlopeCheck()
    {
        Vector2 checkPos = new Vector2(transform.position.x, transform.position.y + offset);
        SlopeCheckHorizontal(checkPos);
        SlopeCheckVertical(checkPos);
    }
    private void SlopeCheckHorizontal(Vector2 checkPos)
    {
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, transform.right, slopeCheckDistance, groundLayer);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -transform.right, slopeCheckDistance, groundLayer);
        if (!PlayerMovementInfo.isCollidingWithPlatform)
        {
            if (slopeHitFront)
            {
                Player.isOnSlope = true;
                PlayerMovementInfo.slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);
            }
            else if (slopeHitBack)
            {
                Player.isOnSlope = true;
                PlayerMovementInfo.slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
            }
        }
        else
        {
            PlayerMovementInfo.slopeSideAngle = 0.0f;
            Player.isOnSlope = false;
        }
    }

    void SlopeCheckVertical(Vector2 checkPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, slopeCheckDistance, groundLayer);
        if (hit)
        {
            PlayerMovementInfo.slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;
            PlayerMovementInfo.slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);
            var flag1 = PlayerMovementInfo.slopeNormalPerp != Vector2.up && PlayerMovementInfo.slopeNormalPerp != Vector2.down;
            var flag2 = PlayerMovementInfo.slopeNormalPerp != Vector2.left && PlayerMovementInfo.slopeNormalPerp != Vector2.right;
            if (!PlayerMovementInfo.isCollidingWithPlatform && (flag1 && flag2) && PlayerMovementInfo.slopeSideAngle == PlayerMovementInfo.lastSlopeAngle)
            {
                Player.isOnSlope = true;
                Debug.DrawRay(hit.point, PlayerMovementInfo.slopeNormalPerp, Color.blue);
                Debug.DrawRay(hit.point, hit.normal, Color.green);
            }
            else
            {
                Player.isOnSlope = false;
            }
            PlayerMovementInfo.lastSlopeAngle = PlayerMovementInfo.slopeDownAngle;
        }
        if (PlayerMovementInfo.slopeDownAngle > PlayerMovementInfo.maxSlopeAngle || PlayerMovementInfo.slopeSideAngle > PlayerMovementInfo.maxSlopeAngle)
        {   
            PlayerMovementInfo.canWalkOnSlope = false;
        }
        else
        {
            PlayerMovementInfo.canWalkOnSlope = true;
        }

        if (Player.isOnSlope && PlayerMovementInfo.canWalkOnSlope && PlayerMovementInfo.xInput == 0.0f)
        {
            rb.sharedMaterial = fullFriction;
        }
        else
        {
            rb.sharedMaterial = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector2(transform.position.x, transform.position.y + offset), new Vector2(transform.position.x + slopeCheckDistance, transform.position.y + offset));
    }
}
