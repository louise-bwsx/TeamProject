using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpwan : MonoBehaviour
{
    public GameObject[] monster;
    public float spwantime = 2f;//每次生成時間
    private float resettime = 0;
    public BossHealth bossHealth;
    void Update()
    {
        resettime += Time.deltaTime;
        if (resettime >= spwantime)
        {
            //只有第二階段會生成
            if (bossHealth.Hp <= bossHealth.maxHp * 0.7 && bossHealth.Hp > bossHealth.maxHp * 0.3)
            { 
                Spwanmonster();
                resettime = 0;
            }
        }
    }
    void Spwanmonster()
    {
        float x = Random.Range(72f, 82f);
        float z = Random.Range(67f, 77f);
        int random = Random.Range(0, 2);
        Instantiate(monster[random], new Vector3(x, 16f, z), transform.rotation);//如果monster是地圖上的物件那生成的位置會被他影響
    }
}
