using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpriteFlip : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public Transform spriterotation;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (spriterotation.localEulerAngles.y < 180)
        {
            //怪物圖片向右看
            spriteRenderer.flipX = true;
        }
        else
        {
            //向左看
            spriteRenderer.flipX = false;
        }
    }
}
