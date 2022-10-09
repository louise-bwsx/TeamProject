using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    public Transform shootingposition;//射擊位置
    public GameObject arrow;//射擊物件
    GameObject shootingarrow;//儲存生成出來的射擊物件
    public float force;//射擊力量
    float forcelimit = 2000;//最大力量
    int arrowlimit = 10;//箭矢數量 未實裝

    void Update()
    {
        if (Input.GetMouseButton(0) && arrowlimit >= 1)
        {
            force += 2000 * Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (force >= forcelimit)
            {
                force = forcelimit;
            }
            Debug.Log("弓箭射出,force: " + force);
            shootingarrow = Instantiate(arrow, shootingposition.position, transform.rotation);
            shootingarrow.GetComponent<Rigidbody>().AddForce(transform.forward * force);
            force = 0;
        }
    }
}
