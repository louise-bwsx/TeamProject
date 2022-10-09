using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpriteFlip : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public GameObject monsterParent;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if(monsterParent.transform.localEulerAngles.y>90 && monsterParent.transform.localEulerAngles.y<270)
        {
            //向左看
            spriteRenderer.flipX = false;
        }
        else
        {
            //怪物圖片向右看
            spriteRenderer.flipX = true;
        }
    }
    void Dead()//動畫Event呼叫
    {
        Destroy(monsterParent);
    }
}
