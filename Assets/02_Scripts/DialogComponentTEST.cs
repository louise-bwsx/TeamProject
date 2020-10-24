using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogComponentTEST : MonoBehaviour
{
    public List<string> introDialog;

    //public List<string> 
    //public string dialog7 = "稻禾：快到了，就在裡面。";
    //private string dialog8 = "稻禾：等等就像這樣，嘿呀，直接打爆他。";
    //private string dialog9 = "稻禾：我會幫你壓制他的力量。你就當作練練手吧。";
    //private string dialog10 = "稻禾：............";
    //private string dialog11 = "稻禾：你說是像怎樣?";
    //private string dialog12 = "稻禾：蛤?";
    //private string dialog13 = "稻禾：敢頂嘴?";
    //private string dialog14 = "稻禾：回來給你好看。";

    //private string dialog15 = "BOSS：啊!";
    //private string dialog16 = "BOSS：(看了看自己)";
    //private string dialog17 = "BOSS：謝謝你的幫忙。";
    //private string dialog18 = "稻禾：好了，你先回來一趟吧!";
    //private string dialog19 = "BOSS：诶?你給我使者令牌幹嘛?";
    //private string dialog20 = "(狐狸轉身離去)";
    //private string dialog21 = "稻禾：你要去哪裡?";
    //private string dialog22 = "稻禾：給我回來!";
    //private string dialog23 = "Fin";

    public int dialogState = 1;
    public Text dialogText;
    public GameObject dialogImage;
    public GameMenu gameMenu;
    void Start()
    {
        introDialog.Add("稻荷神：快到了，就在裡面。");
        introDialog.Add("稻荷神：等等就像這樣，嘿呀，直接打爆他。");
        introDialog.Add("稻荷神：我會幫你壓制他的力量。你就當作練練手吧。");
        introDialog.Add("稻荷神：............");
        introDialog.Add("稻荷神：你說是像怎樣?");
        introDialog.Add("稻荷神：蛤?");
        introDialog.Add("稻荷神：敢頂嘴");
        introDialog.Add("稻荷神：回來給你好看。");

        //public string dialog7 = "稻禾：快到了，就在裡面。";
        //private string dialog8 = "稻禾：等等就像這樣，嘿呀，直接打爆他。";
        //private string dialog9 = "稻禾：我會幫你壓制他的力量。你就當作練練手吧。";
        //private string dialog10 = "稻禾：............";
        //private string dialog11 = "稻禾：你說是像怎樣?";
        //private string dialog12 = "稻禾：蛤?";
        //private string dialog13 = "稻禾：敢頂嘴?";
        //private string dialog14 = "稻禾：回來給你好看。";

        gameMenu = GetComponent<GameMenu>();
        Time.timeScale = 0;
        dialogText.text = introDialog[0];
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
        if (dialogState < introDialog.Count)
        { 
            dialogText.text = introDialog[dialogState];
        }
        if (dialogState >= introDialog.Count)
        {
            dialogImage.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
