using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollDirection : MonoBehaviour
{
    public PlayerControl playerControl;
    void Update()
    {
        if (playerControl.cantMove == false)
        {
            if (Input.GetAxis("Horizontal") >= 0.1f)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            if (Input.GetAxis("Horizontal") >= 0.1f && Input.GetAxis("Vertical") <= -0.1f)
            {
                transform.rotation = Quaternion.Euler(0, 135, 0);
            }
            else if (Input.GetAxis("Vertical") <= -0.1f)
            {
                transform.rotation = Quaternion.Euler(0, -180, 0);
            }
            if (Input.GetAxis("Vertical") <= -0.1f && Input.GetAxis("Horizontal") <= -0.1f)
            {
                transform.rotation = Quaternion.Euler(0, -135, 0);
            }
            else if (Input.GetAxis("Horizontal") <= -0.1f)
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
            }
            if (Input.GetAxis("Horizontal") <= -0.1f && Input.GetAxis("Vertical") >= 0.1f)
            {
                transform.rotation = Quaternion.Euler(0, -45, 0);
            }
            else if (Input.GetAxis("Vertical") >= 0.1f)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            if (Input.GetAxis("Horizontal") >= 0.1f && Input.GetAxis("Vertical") >= 0.1f)
            {
                transform.rotation = Quaternion.Euler(0, 45, 0);
            }
        }
    }
}
