using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public GameObject[] anyWindow = new GameObject[4];
    public GetHitEffect getHitEffect;
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
            EscButton();
        }
    }
    public void EscButton()
    {
        //當背包是開的 或是 教學介面是開著的
        if (anyWindow[3].activeSelf || anyWindow[7].activeSelf)
        {
            //把他們關起來
            anyWindow[3].SetActive(false);
            anyWindow[7].SetActive(false);
        }

        else if (!anyWindow[0].activeSelf)
        {
            //如果主選單是關閉的
            //for迴圈關閉所有視窗
            foreach (GameObject window in anyWindow)
            {
                window.SetActive(false);
            }
            //再把主選單打開
            anyWindow[0].SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {   
            //主選單是開著的就把它關掉
            anyWindow[0].SetActive(false);
            if (getHitEffect.playerHealth > 0)
            { 
                Time.timeScale = 1f;
            }
        }
    }
    public void GameContinue()
    {
        //綁在button上
        if (getHitEffect.playerHealth > 0)
        { 
            Time.timeScale = 1f;
        }
    }

    public void QuitGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}