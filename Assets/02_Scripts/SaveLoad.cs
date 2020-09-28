using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    float playerPosX;
    float playerPosY;
    float playerPosZ;

    string playerName;

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
            playerName = player.GetComponent<PlayerStatus>().playerName;
            playerPosX = player.GetComponent<PlayerStatus>().playerPosX;
            playerPosY = player.GetComponent<PlayerStatus>().playerPosY;
            playerPosZ = player.GetComponent<PlayerStatus>().playerPosZ;
            //playerPos = player.GetComponent<Transform>().position;
            PlayerPrefs.SetString("playerName", playerName);
            PlayerPrefs.SetFloat("playerPosX", playerPosX);
            PlayerPrefs.SetFloat("playerPosY", playerPosY);
            PlayerPrefs.SetFloat("playerPosZ", playerPosZ);
            SceneManager.LoadScene(sceneName);
        }
    }
}
