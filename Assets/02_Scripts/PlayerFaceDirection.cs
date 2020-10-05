using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFaceDirection : MonoBehaviour
{
    public PlayerControl playerControl;
    public SpriteRenderer spriteRenderer;
    bool isRight = true;
    void Update()
    {
        if (playerControl.cantMove == false)
        { 
            if (Input.GetAxis("Horizontal") >= 0.1f)
            {
                spriteRenderer.flipX = isRight;
            }
            if (Input.GetAxis("Horizontal") <= -0.1f)
            {
                spriteRenderer.flipX = !isRight;
            }
        }
    }
}
