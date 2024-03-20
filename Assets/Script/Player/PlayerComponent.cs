using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponent : MonoBehaviour
{
    protected static Rigidbody2D rb;
    protected static CapsuleCollider2D col;
    protected static Animator anim;
    protected static SpriteRenderer sr;
    protected static LineRenderer lr;
    // Start is called before the first frame update
    void Awake()
    {
        Initialization();
    }

    protected virtual void Initialization()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        col = GetComponentInChildren<CapsuleCollider2D>();
        anim = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
        lr = GetComponentInChildren<LineRenderer>();
    }
}
