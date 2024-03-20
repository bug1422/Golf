using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerMovement : PlayerComponent
{
    [SerializeField]
    protected float speed = 10f;
    [SerializeField]
    protected float jumpStrength = 1000f;
    [SerializeField]
    private float groundCheckRadius;
    [SerializeField]
    private Transform groundCheck;
    private bool canJump;
    private Vector2 velocity;
    private Vector2 jumpForce;
    private LayerMask groundLayer;
    protected override void Initialization()
    {
        groundLayer = GetComponent<Layers>().GetGround();
        base.Initialization();
    }

    // Start is called before the first frame update


    // Update is called once per frame
    protected void Update()
    {
        if (Player.isAlive && !Player.isGrabbingLedge && !(Player.isHolding || Player.isPlaying))
        {
            PlayerMovementInfo.xInput = Input.GetAxisRaw("Horizontal");
            if (PlayerMovementInfo.xInput == 1 && !Player.isFacingRight) Flip();
            else if (PlayerMovementInfo.xInput == -1 && Player.isFacingRight) Flip();
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }
        Globals.playerTransform = transform;
    }

    protected void FixedUpdate()
    {
        CheckGround();
        ApplyMovement();
    }

    private void Flip()
    {
        Player.isFacingRight = !Player.isFacingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    private void CheckGround()
    {
        Player.isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (Player.isFallingThroughPlatform && rb.velocity.y != 0.0f)
        {
            Player.isGrounded = false;
        }
        if (rb.velocity.y <= 0.0f)
        {
            Player.isJumping = false;
        }

        if (Player.isGrounded && !Player.isJumping && PlayerMovementInfo.slopeDownAngle <= PlayerMovementInfo.maxSlopeAngle)
        {
            canJump = true;
        }
    }
    private void ApplyMovement()
    {
        if (Player.isAlive)
        {
            if ((Player.isHolding || Player.isPlaying) && Player.isGrounded)
            {
                velocity = new Vector2(0.0f, 0.0f);
                rb.velocity = velocity;
            }
            else if (!Player.isGrabbingLedge)
            {
                if (Player.isGrounded && !Player.isOnSlope && !Player.isJumping) //if not on slope
                {
                    velocity = new Vector2(speed * PlayerMovementInfo.xInput, 0.0f);
                    rb.velocity = velocity;
                }
                else if (Player.isGrounded && Player.isOnSlope && PlayerMovementInfo.canWalkOnSlope && !Player.isJumping && !Player.isFallingThroughPlatform) //If on slope
                {
                    velocity = new Vector2(speed * PlayerMovementInfo.slopeNormalPerp.x * -PlayerMovementInfo.xInput, speed * PlayerMovementInfo.slopeNormalPerp.y * -PlayerMovementInfo.xInput);
                    rb.velocity = velocity;
                }
                else if (!Player.isGrounded) //If in air
                {
                    velocity = new Vector2(speed * PlayerMovementInfo.xInput, rb.velocity.y);
                    rb.velocity = velocity;
                }
            }
        }
    }


    protected void Jump()
    {
        if (canJump)
        {
            canJump = false;
            Player.isJumping = true;
            velocity.Set(0.0f, 0.0f);
            rb.velocity = velocity;
            jumpForce.Set(0.0f, jumpStrength);
            rb.AddForce(jumpForce, ForceMode2D.Impulse);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
    public void Kill()
    {
        Destroy(gameObject);
    }
}
