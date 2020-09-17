using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFaceDirection : MonoBehaviour
{
    public PlayerControl playerControl;
    void Update()
    {

        if (Input.GetAxis("Horizontal") >= 0.1f && playerControl.cantMove == false)
        {
            transform.rotation = Quaternion.Euler(Vector3.up * 90);
        }
        if (Input.GetAxis("Horizontal") >= 0.1f && Input.GetAxis("Vertical") <= -0.1f && playerControl.cantMove == false)
        {
            transform.rotation = Quaternion.Euler(Vector3.up * 135);
        }
        else if (Input.GetAxis("Vertical") <= -0.1f && playerControl.cantMove == false)
        {
            transform.rotation = Quaternion.Euler(Vector3.up * 180);
        }
        if (Input.GetAxis("Vertical") <= -0.1f && Input.GetAxis("Horizontal") <= -0.1f && playerControl.cantMove == false)
        {
            transform.rotation = Quaternion.Euler(Vector3.up * -135);
        }
        else if (Input.GetAxis("Horizontal") <= -0.1f && playerControl.cantMove == false)
        {
            transform.rotation = Quaternion.Euler(Vector3.up * -90);
        }
        if (Input.GetAxis("Vertical") >= 0.1f && Input.GetAxis("Horizontal") <= -0.1f && playerControl.cantMove == false)
        {
            transform.rotation = Quaternion.Euler(Vector3.up * -45);
        }
        else if (Input.GetAxis("Vertical") >= 0.1f && playerControl.cantMove == false)
        {
            transform.rotation = Quaternion.Euler(Vector3.up * 0);
        }
        if (Input.GetAxis("Vertical") >= 0.1f && Input.GetAxis("Horizontal") >= 0.1f && playerControl.cantMove == false)
        {
            transform.rotation = Quaternion.Euler(Vector3.up * 45);
        }
    }
}
