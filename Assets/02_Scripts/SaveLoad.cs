using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoad : MonoBehaviour
{
    float playerPosX;
    float playerPosY;
    float playerPosZ;

    string playerName;

    public string sceneName;

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            PlayerPrefs.SetFloat("playerPosX", playerPosX);
            PlayerPrefs.SetFloat("playerPosY", playerPosY);
            PlayerPrefs.SetFloat("playerPosZ", playerPosZ);

            PlayerPrefs.SetString("playerName", playerName);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            playerName = gameObject.GetComponent<GameObject>().name;
            playerPosX = gameObject.GetComponent<Transform>().transform.position.x;
            playerPosY = gameObject.GetComponent<Transform>().transform.position.y;
            playerPosZ = gameObject.GetComponent<Transform>().transform.position.z;
            //playerPos = player.GetComponent<Transform>().position;
            PlayerPrefs.SetString("playerName", playerName);
            PlayerPrefs.SetFloat("playerPosX", playerPosX);
            PlayerPrefs.SetFloat("playerPosY", playerPosY);
            PlayerPrefs.SetFloat("playerPosZ", playerPosZ);
            SceneManager.LoadScene(sceneName);
        }
    }
}
