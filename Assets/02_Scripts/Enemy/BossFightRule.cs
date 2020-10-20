using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightRule : MonoBehaviour
{
    public int bossFightState = 1;
    public Collider guardArea;
    public Transform miniosGroup;
    void Update()
    {
        switch (bossFightState)
        {
            case 1:
                {
                    if (miniosGroup.childCount == 0)
                    {
                        guardArea.isTrigger = false;
                    }
                    break;
                }
            case 2:
                {
                    break;
                }
        }
        //if (transform.childCount == 0 && bossFightState<=3)
        //{
        //    if (bossFightState != 3)
        //    {
        //        Instantiate(guardStatueRespwan, transform.position, transform.rotation, transform);
        //    }
        //    if (bossFightState == 3)
        //    {
        //        //生成不含怪物的雕像
        //        finalStatue = Instantiate(guardStatue, transform.position + Vector3.up*1.6f, transform.rotation,transform);
        //    }
        //}
    }
}
