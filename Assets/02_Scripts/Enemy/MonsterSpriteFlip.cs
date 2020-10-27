using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpriteFlip : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public Transform spriterotation;
    public GameObject monsterParent;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        Debug.Log(spriterotation.localEulerAngles.y);
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
    void Dead()//動畫Event呼叫
    {
        Destroy(monsterParent);
    }
}
