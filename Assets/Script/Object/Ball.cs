using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    public float power = 5f;
    [SerializeField]
    public float stepDistance = 10;
    private Rigidbody2D rb;
    private LineRenderer lr;
    private Vector2 DragStartPos;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Player.isHolding = true;
            DragStartPos = transform.position;
        }
        if (Input.GetMouseButton(0))
        {
            Vector2 DragEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 _velocity = (DragEndPos - DragStartPos) * power;
            print(_velocity);
            Vector2[] trajectory = Plot((Vector2) transform.position, _velocity, 50);
            Vector3[] positions = new Vector3[trajectory.Length];
            for (int i = 0; i < positions.Length; i++)
            {
                if (trajectory[i] != Vector2.zero) positions[i] = trajectory[i];
            }
            lr.positionCount = positions.Length;
            lr.SetPositions(positions);
        }
        if (Input.GetMouseButtonUp(0))
        {
            lr.positionCount = 0;
            Vector2 DragEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 _velocity = (DragEndPos - DragStartPos) * power;
            SpawnBall(_velocity);
        }
    }
    private void SpawnBall(Vector2 _velocity)
    {
        Player.isHolding = false;
        Player.isPlaying = true;
        var instance = this.gameObject;
        instance.GetComponent<Rigidbody2D>().velocity = _velocity;
        Instantiate(instance, transform.position, Quaternion.identity);
    }
    public Vector2[] Plot(Vector2 pos, Vector2 velocity, int step)
    {
        Vector2[] result = new Vector2[step];
        float timeStep = Time.fixedDeltaTime / Physics2D.velocityIterations * stepDistance;
        Vector2 gravityAccel = Physics2D.gravity * rb.gravityScale * timeStep * timeStep;
        float drag = 1f - timeStep * rb.drag;
        Vector2 moveStep = velocity * timeStep;
        for (int i = 0; i < step; i++)
        {
            moveStep += gravityAccel;
            moveStep *= drag;
            pos += moveStep;
        }
        return result;
    }
}
