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
        //Debug.Log(spriterotation.localEulerAngles.y);//會以很慢的速度遞減感覺不准
        //Debug.Log(spriterotation.eulerAngles);
        if (spriterotation.localEulerAngles.y < 90 || spriterotation.localEulerAngles.y>270)
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
