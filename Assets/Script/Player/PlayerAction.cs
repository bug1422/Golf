using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField]
    public float power = 5f;
    [SerializeField]
    public float offset = 1f;
    [SerializeField]
    public float bound = 10f;
    Vector2 DragStartPos;
    [SerializeField]
    protected AnimationClip clip;
    [SerializeField]
    private GameObject ball;
    private Rigidbody2D rb;
    private LineRenderer lr;

    void Start()
    {
        rb = ball.GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
    }
    void Update()
    {
        if (Player.isAlive && Player.isGrounded)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Player.isHolding = true;
                DragStartPos = transform.position;
            }
            if (Player.isHolding && Input.GetMouseButton(0))
            {
                Vector2 DragEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 _velocity = (DragEndPos - DragStartPos) * power;
                Vector2[] trajectory = Plot(new Vector2(transform.position.x, transform.position.y - offset), _velocity, 50);
                Vector3[] positions = new Vector3[trajectory.Length];
                for (int i = 0; i < positions.Length; i++)
                {
                    if (trajectory[i] != Vector2.zero) positions[i] = trajectory[i];
                }
                lr.positionCount = positions.Length;
                lr.SetPositions(positions);
            }
            if (Player.isHolding && Input.GetMouseButtonUp(0))
            {
                lr.positionCount = 0;
                Vector2 DragEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 _velocity = (DragEndPos - DragStartPos) * power;
                SpawnBall(_velocity);
            }
        }
    }
    private void SpawnBall(Vector2 _velocity)
    {
        Player.isHolding = false;
        Player.isPlaying = true;
        var instance = Instantiate(ball, new Vector2(transform.position.x, transform.position.y - offset), Quaternion.identity);
        instance.GetComponent<Rigidbody2D>().velocity = _velocity;
        StartCoroutine(Play());
    }
    private IEnumerator Play()
    {
        yield return new WaitForSeconds(clip.length);
        Player.isPlaying = false;
    }

    public Vector2[] Plot(Vector2 pos, Vector2 velocity, int step)
    {
        Vector2[] result = new Vector2[step];
        float timeStep = Time.fixedDeltaTime / Physics2D.velocityIterations;
        Vector2 gravityAccel = Physics2D.gravity * rb.gravityScale * timeStep * timeStep;
        float drag = 1f - timeStep * rb.drag;
        Vector2 moveStep = velocity * timeStep;
        for (int i = 0; i < step; i++)
        {
            moveStep += gravityAccel;
            moveStep *= drag;
            pos += moveStep;
            /*
            if(Mathf.Abs(pos.x) > transform.position.x + bound)
            {
                if(i!=0)result[i] = result[i - 1];
            }
            else if (Mathf.Abs(pos.y) > transform.position.y + bound)
            {
                if (i != 0) result[i] = result[i - 1];
            }
            else result[i] = pos;*/
            result[i] = pos;
        }
        return result;
    }
}
