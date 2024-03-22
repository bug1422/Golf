using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlopeHandler : MonoBehaviour
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
    [SerializeField]
    private LayerMask groundLayer;
    private Enemy enemy;
    private EnemyMovementInfo info;
    private Rigidbody2D rb;
    private void Start()
    {
        enemy = GetComponent<Enemy>();
        info = GetComponent<EnemyMovementInfo>();
        rb = GetComponent<Rigidbody2D>();
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
        info.isCollidingWithPlatform = false;
        if (hit)
        {
            if (hit.collider.CompareTag("Platform"))
            {
                Debug.DrawRay(transform.position, Vector2.down * collideDistance, Color.black);
                info.isCollidingWithPlatform = true;
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
        if (!info.isCollidingWithPlatform)
        {
            if (slopeHitFront)
            {
                enemy.isOnSlope = true;
                info.slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);
            }
            else if (slopeHitBack)
            {
                enemy.isOnSlope = true;
                info.slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
            }
        }
        else
        {
            info.slopeSideAngle = 0.0f;
            enemy.isOnSlope = false;
        }
    }

    void SlopeCheckVertical(Vector2 checkPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, slopeCheckDistance, groundLayer);
        if (hit)
        {
            info.slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;
            info.slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);
            var flag1 = info.slopeNormalPerp != Vector2.up && info.slopeNormalPerp != Vector2.down;
            var flag2 = info.slopeNormalPerp != Vector2.left && info.slopeNormalPerp != Vector2.right;
            if (!info.isCollidingWithPlatform && (flag1 && flag2) && info.slopeSideAngle == info.lastSlopeAngle)
            {
                enemy.isOnSlope = true;
                Debug.DrawRay(hit.point, info.slopeNormalPerp, Color.blue);
                Debug.DrawRay(hit.point, hit.normal, Color.green);
            }
            else
            {
                enemy.isOnSlope = false;
            }
            info.lastSlopeAngle = info.slopeDownAngle;
        }
        if (info.slopeDownAngle > info.maxSlopeAngle || info.slopeSideAngle > info.maxSlopeAngle)
        {
            info.canWalkOnSlope = false;
        }
        else
        {
            info.canWalkOnSlope = true;
        }

        if (enemy.isOnSlope && info.canWalkOnSlope && info.xInput == 0.0f)
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
