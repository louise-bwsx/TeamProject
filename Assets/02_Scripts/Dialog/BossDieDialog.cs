using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossDieDialog : MonoBehaviour
{
    public List<string> bossDieDialog;

    public int dialogState = 1;
    public Text dialogText;
    public GameObject dialogImage;
 
    void Start()
    {
        bossDieDialog.Add("BOSS：啊!");
        bossDieDialog.Add("BOSS：(看了看自己)");
        bossDieDialog.Add("BOSS：謝謝你的幫忙。");
        bossDieDialog.Add("稻禾：好了，你先回來一趟吧!");
        bossDieDialog.Add("BOSS：诶?你給我使者令牌幹嘛?");
        bossDieDialog.Add("(狐狸轉身離去)");
        bossDieDialog.Add("稻禾：你要去哪裡?");
        bossDieDialog.Add("稻禾：給我回來!");
        bossDieDialog.Add("Fin");

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
        if (dialogState < bossDieDialog.Count)
        { 
            dialogText.text = bossDieDialog[dialogState];
        }
        if (dialogState >= bossDieDialog.Count)
        {
            dialogImage.SetActive(false);
            Time.timeScale = 1;
            SceneManager.LoadScene("MenuScene");
        }
      
    }
}
