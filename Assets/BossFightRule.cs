using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightRule : MonoBehaviour
{
    public int bossFightState = 1;
    //有太多奇奇怪怪的bug
    //public Transform minionsGroup;
    //public GameObject guardStatue;

    public GameObject statueRespwan;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == 0)
        {
            //Instantiate(minionsGroup, transform.position, transform.rotation,transform);
            //Instantiate(guardStatue, transform.position, transform.rotation,transform);
            Instantiate(statueRespwan, transform.position, transform.rotation, transform);
            bossFightState++;
        }
    }
}
