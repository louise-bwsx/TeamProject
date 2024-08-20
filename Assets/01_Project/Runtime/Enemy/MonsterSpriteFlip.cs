using UnityEngine;

public class MonsterSpriteFlip : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject monsterParent;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (monsterParent.transform.localEulerAngles.y > 90 && monsterParent.transform.localEulerAngles.y < 270)
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

    private void Dead()//動畫Event呼叫
    {
        Destroy(monsterParent);
    }
}