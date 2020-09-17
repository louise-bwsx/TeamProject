using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneDestroy : MonoBehaviour
{
    int breaklimite = 10;
    int breakvalue = 0;
    public GameObject gold;
    public Transform goldmine;

    private void OnTriggerEnter(Collider other)
    {
        breakvalue++;
        if (other.gameObject.CompareTag("Pickaxe"))
        {
            Debug.Log("break: " + breakvalue);
            breakvalue += 2;
        }
        if (breakvalue >= breaklimite)
        {
            Debug.Log("finish");
            Destroy(gameObject);
            gold = Instantiate(gold, goldmine.position, transform.rotation);
        }
    }
    public void OnMouseDown()
    {
        Debug.Log("滑鼠點到有這Script的物件");
    }
}
