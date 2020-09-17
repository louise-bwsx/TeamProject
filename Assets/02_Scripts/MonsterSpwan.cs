using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpwan : MonoBehaviour
{
    //public UIText uitext;//生成點++
    public int monstervalue;
    public GameObject monster;
    public GameObject spwanmonster;//變成像是生怪點 但只有y會被影響 xz不會
    private float spwantime = 2f;
    private float resettime = 0;
    void Start()
    {
        
    }

    void Update()
    {
        resettime += Time.deltaTime;
        if (resettime >= spwantime)
        {
            Spwanmonster();
            resettime = 0;
        }
    }
    void Spwanmonster()
    {
        float x = Random.Range(9.0f, 21.0f);
        float z = Random.Range(-3.5f, 3.5f);
        spwanmonster = Instantiate(monster, new Vector3(x, 0.7f, z), transform.rotation);//如果monster是地圖上的物件那生成的位置會被他影響
        monstervalue++;
        Debug.Log("monster生成,共 " + monstervalue + " 隻");
    }
}
