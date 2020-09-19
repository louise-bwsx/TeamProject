using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public GameObject[] anyWindow = new GameObject[3];
    void Start()
    {
        foreach (GameObject window in anyWindow)
        {
            window.SetActive(false);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            EscButton();
        }

    }
    public void EscButton()
    {
        if (!anyWindow[0].activeSelf)
        {
            //for所有的視窗SetActive(false)
            foreach (GameObject window in anyWindow)
            {
                window.SetActive(false);
            }
            anyWindow[0].SetActive(true);
        }
        else
        {
            anyWindow[0].SetActive(false);
            Time.timeScale = 1f;
        }
    }
    public void GameContinue()
    {
        anyWindow[0].SetActive(false);
        Time.timeScale = 1f;
    }
    public void QuitGame()
    {
        SceneManager.LoadScene(0) ;
        //聽說build出來真的會結束但是在unity裡面不會有任何作用所以用debug.log來代替
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
