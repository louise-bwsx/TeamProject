using UnityEngine;

public class GameMenu : MonoBehaviour
{
    public GameObject[] anyWindow = new GameObject[4];
    public GameObject[] startOpenWindow;
    public MobileStats mobileStats;
    void Start()
    {
        mobileStats = FindObjectOfType<MobileStats>();
        foreach (GameObject window in anyWindow)
        {
            window.SetActive(false);
        }
        //因為開頭的字幕打開修改UI很礙眼
        //但不打開匯出的時候會忘記開
        foreach (GameObject window in startOpenWindow)
        {
            window.SetActive(true);
        }
    }
    public void EscButton()//按鈕選擇
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
            if (mobileStats.hp > 0)
            {
                Time.timeScale = 1f;
            }
        }
    }
    public void GameContinue()
    {
        //綁在button上
        if (mobileStats.hp > 0)
        {
            Time.timeScale = 1f;
        }
    }
    public void QuitGame()
    {
        Debug.LogError("這邊我猜是回到Menu");
        //SceneManager.LoadScene(0);
    }
}
