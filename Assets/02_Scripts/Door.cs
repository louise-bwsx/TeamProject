using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform door;
    public GameObject boss;
    public bool isDoorClose = false;
    private void Start()
    {
        boss.SetActive(false);
    }
    private void Update()
    {
        if (boss == null)
        {
            door.localPosition = new Vector3(-0.23f, 5.23f, 3.3f);
            isDoorClose = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && boss != null)
        {
            isDoorClose = true;
            door.localPosition = new Vector3(4.52f, 5.23f, 3.3f);
            boss.SetActive(true);
        }
    }
}
