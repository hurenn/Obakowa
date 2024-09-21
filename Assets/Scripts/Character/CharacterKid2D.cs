using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterKid2D : CharacterKid
{
    private SpriteRenderer rend;

    public override void Setup()
    {
        base.Setup();
        rend = GetComponentInChildren<SpriteRenderer>();
    }

    protected override void _Update()
    {
        if(rb.velocity.magnitude < 0.1f)
        {
            return;
        }

        if (rb.velocity.x > 0.1f)
        {
            rend.flipX = false;
        }
        else if(rb.velocity.x < -0.1f)
        {
            rend.flipX = true;
        }
    }

    public override void Failed()
    {
        enable_shoot++;
        enable_move++;
        enable_action++;

        rend.material.color = Color.gray;
    }
}
