using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLedge : PlayerComponent
{
    [SerializeField]
    protected AnimationClip clip;

    [SerializeField]
    protected float climbingHorizontalOffset = .5f;
    protected float climbingVerticalOffset = .5f;

    private Vector2 topOfPlayer;
    private GameObject ledge;
    private float animationTime = .5f;
    private bool moved;
    private bool climbing;
    [SerializeField]
    private float detectDistance = .2f;
    private LayerMask platformLayer;
    protected override void Initialization()
    {
        base.Initialization();
        if (clip != null)
        {
            animationTime = clip.length;
        }
        platformLayer = GetComponent<Layers>().GetPlatform();
    }
    private void FixedUpdate()
    {
        CheckForLedge();
        LedgeHanging();
    }

    protected virtual void CheckForLedge()
    {
        if (!Player.isGrounded)
        {
            if (Player.isFacingRight)
            {
                topOfPlayer = new Vector2(col.bounds.center.x, col.bounds.max.y);
                RaycastHit2D hit = Physics2D.Raycast(topOfPlayer, Vector2.right, detectDistance, platformLayer);
                if (hit) print(hit.collider.gameObject);
                if (hit && hit.collider.gameObject.GetComponent<Ledge>())
                {
                    ledge = hit.collider.gameObject;
                    Collider2D ledgeCollider = ledge.GetComponent<Collider2D>();
                    Player.isGrabbingLedge = true;
                }

            }
            else
            {
                topOfPlayer = new Vector2(col.bounds.center.x, col.bounds.max.y);
                RaycastHit2D hit = Physics2D.Raycast(topOfPlayer, Vector2.left, detectDistance, platformLayer);
                if (hit) print(hit.collider.gameObject);
                if (hit && hit.collider.gameObject.GetComponent<Ledge>())
                {
                    ledge = hit.collider.gameObject;
                    Collider2D ledgeCollider = ledge.GetComponent<Collider2D>();
                    Player.isGrabbingLedge = true;
                }
            }
        }
        if (ledge != null && Player.isGrabbingLedge)
        {
            AdjustPlayerPosition();
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    protected virtual void LedgeHanging()
    {
        if (Player.isGrabbingLedge && Input.GetAxis("Vertical") > 0 && !climbing)
        {
            climbing = true;
            if (Player.isFacingRight)
            {
                StartCoroutine(ClimbingLedge(new Vector2(transform.position.x + climbingHorizontalOffset, ledge.GetComponent<Collider2D>().bounds.max.y + col.bounds.extents.y + climbingVerticalOffset), animationTime));
            }
            else
            {
                StartCoroutine(ClimbingLedge(new Vector2(transform.position.x - climbingHorizontalOffset, ledge.GetComponent<Collider2D>().bounds.max.y + col.bounds.extents.y + climbingVerticalOffset), animationTime));
            }
        }
        if (Player.isGrabbingLedge && Input.GetAxis("Vertical") < 0)
        {
            ledge = null;
            moved = false;
            Player.isGrabbingLedge = false;
            rb.bodyType = RigidbodyType2D.Dynamic;
            transform.Translate(new Vector2(0, -0.5f));
        }
    }

    protected virtual IEnumerator ClimbingLedge(Vector2 topOfPlatform, float duration)
    {
        float time = 0;
        Vector2 startValue = transform.position;
        while (time <= duration)
        {
            Player.isClimbing = true;
            transform.position = Vector2.Lerp(startValue, topOfPlatform, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        ledge = null;
        moved = false;
        climbing = false;
        Player.isGrabbingLedge = false;
        Player.isClimbing = false;
    }
    protected virtual void AdjustPlayerPosition()
    {
        if (!moved)
        {
            moved = true;
            Collider2D ledgeCollider = ledge.GetComponent<Collider2D>();
            Ledge platform = ledge.GetComponent<Ledge>();
            transform.position = new Vector2(transform.position.x, (ledgeCollider.bounds.max.y - col.bounds.extents.y - .5f) + platform.hangingVerticalOffset);
            if (Player.isFacingRight)
            {
                if (col.bounds.max.y < ledgeCollider.bounds.max.y && col.bounds.center.x < ledgeCollider.bounds.min.x)
                {
                    transform.position = new Vector2((ledgeCollider.bounds.min.x - col.bounds.extents.x) + platform.hangingHorizontalOffset, transform.position.y);
                }
            }
            else
            {
                if (col.bounds.max.y < ledgeCollider.bounds.max.y && col.bounds.center.x > ledgeCollider.bounds.max.x)
                {
                    transform.position = new Vector2((ledgeCollider.bounds.max.x + col.bounds.extents.x) - platform.hangingHorizontalOffset, transform.position.y);
                }
            }
        }
    }
}
