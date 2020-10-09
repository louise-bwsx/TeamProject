using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform door;
    public GameObject boss;
    private void Update()
    {
        if (boss == null)
        {
            door.localPosition = new Vector3(-0.23f, 5.23f, 3.3f);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && boss != null)
        {
            door.localPosition = new Vector3(4.52f, 5.23f, 3.3f);
        }
    }
}
