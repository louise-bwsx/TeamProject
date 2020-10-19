using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightRule : MonoBehaviour
{
    public int bossFightState = 1;
    public GameObject guardStatueRespwan;
    public GameObject guardStatue;
    public GameObject finalStatue;
    void Update()
    {
        if (transform.childCount == 0 && bossFightState<=3)
        {
            if (bossFightState != 3)
            {
                Instantiate(guardStatueRespwan, transform.position, transform.rotation, transform);
            }
            if (bossFightState == 3)
            {
                //生成點是父物件的0,0,0 但是以子物件的中心為基準
                finalStatue = Instantiate(guardStatue, transform.position + Vector3.up*1.6f, transform.rotation,transform);
            }
        }
    }
}
