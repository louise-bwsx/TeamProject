using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossDieDialog : MonoBehaviour
{
    public List<string> bossDieDialog;

    public int dialogState = 1;
    public Text dialogText;
    public GameObject dialogImage;
    public Image godImage;
    public GameObject staffImage;

    void Start()
    {
        bossDieDialog.Add("天王：啊!");
        bossDieDialog.Add("天王：(看了看自己)");
        bossDieDialog.Add("天王：謝謝你的幫忙。");
        bossDieDialog.Add("稻荷神：好了，你先回來一趟吧!");
        bossDieDialog.Add("天王：诶?你給我使者令牌幹嘛?");
        bossDieDialog.Add("(狐狸轉身離去)");
        bossDieDialog.Add("稻荷神：你要去哪裡?");
        bossDieDialog.Add("稻荷神：給我回來!");
        bossDieDialog.Add("遊戲結束");
        bossDieDialog.Add("製作人員名單畫面顯現");

        Time.timeScale = 0;
        dialogText.text = bossDieDialog[0];
    }
    void Update()
    {
        if (dialogImage.activeSelf)
        {
            Time.timeScale = 0;
        }
    }
    public void DialogChange()
    {
        dialogState++;
        if (dialogState == 9)
        {
            staffImage.SetActive(true);
        }
        else if (dialogState < bossDieDialog.Count)
        {
            dialogText.text = bossDieDialog[dialogState];
        }
        if (dialogState >= bossDieDialog.Count)
        {
            dialogImage.SetActive(false);
            Time.timeScale = 0;
        }
        //以下是讓頭像出現
        if (dialogState == 3 || dialogState == 6 || dialogState == 7)
        {
            godImage.enabled = true;
        }
        else
        {
            godImage.enabled = false;
        }
    }
    public void LoadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuScene");
    }
}
