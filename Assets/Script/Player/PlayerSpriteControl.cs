using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteControl : PlayerComponent
{
    [SerializeField]
    private Sprite swing;
    // Start is called before the first frame update
    protected override void Initialization()
    {
        base.Initialization();
    }
    // Update is called once per frame
    void Update()
    {
        if (Player.isHolding)
        {
            anim.enabled = false;
            sr.sprite = swing;
            StartCoroutine(Swing());
        }
        else
        {
            if (Player.isAlive) anim.SetFloat("x", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
            anim.SetBool("isGrounded", Player.isGrounded);
            anim.SetBool("isPlaying", Player.isPlaying);
            anim.SetBool("isJumping", Player.isJumping);
            anim.SetBool("isGrabbingLedge", Player.isGrabbingLedge);
            anim.SetBool("isClimbing", Player.isClimbing);
            anim.SetBool("isOnSlope", Player.isOnSlope);
        }
    }

    private IEnumerator Swing()
    {
        yield return new WaitUntil(() => { return Player.isPlaying; });
        anim.enabled = true;
        anim.Play("Golfing");
        //Find another way
        //sr.sprite = sprite;
    }
}
