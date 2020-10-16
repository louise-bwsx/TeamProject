using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectMonster : MonoBehaviour
{
    GameObject door;
    void Start()
    {
        door = GameObject.FindGameObjectWithTag("Door");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == 0)
        {
            door.SetActive(true);
        }
    }
}
