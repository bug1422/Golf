using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
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
    [SerializeField]
    private LayerMask groundLayer;
    private Enemy enemy;
    private EnemyMovementInfo info;
    private Rigidbody2D rb;
    [SerializeField]
    private EdgeCollider2D edge;
    private void Start()
    {
        enemy = GetComponent<Enemy>();
        info = GetComponent<EnemyMovementInfo>();
        rb = GetComponent<Rigidbody2D>();
        info.xInput = 1;
    }
    // Start is called before the first frame update


    // Update is called once per frame

    private void Update()
    {
        GetPosition();
        if (info.xInput == 1 && !enemy.isFacingRight) Flip();
        else if (info.xInput == -1 && enemy.isFacingRight) Flip();
    }
    protected void FixedUpdate()
    {
        CheckGround();
        ApplyMovement();
    }

    private void Flip()
    {
        enemy.isFacingRight = !enemy.isFacingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    private void CheckGround()
    {
        enemy.isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (enemy.isGrounded && !enemy.isJumping && info.slopeDownAngle <= info.maxSlopeAngle)
        {
            canJump = true;
        }
    }
    private void ApplyMovement()
    {
        rb.velocity = new Vector2(speed * info.xInput, 0);
        rb.AddForce(new Vector2(0, -9.8f));
    }
    private void GetPosition()
    {
        var pos =  Mathf.FloorToInt(transform.position.x);
        var start = Mathf.FloorToInt(edge.transform.position.x + edge.points[1].x);
        var end = Mathf.FloorToInt(edge.transform.position.x + edge.points[0].x);
        if (pos == end)
        {
            info.xInput = 1;
        }
        if (pos == start)
        {
            info.xInput = -1;
        }
        print(pos + " " + end + " " + start);
    }
}
