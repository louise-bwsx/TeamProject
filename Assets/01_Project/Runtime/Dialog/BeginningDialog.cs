using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeginningDialog : MonoBehaviour
{
    public List<string> beginningDialog;

    public int dialogState = 1;
    public Text dialogText;
    public GameObject dialogImage;
    void Start()
    {
        beginningDialog.Add("稻荷神：快到了，就在裡面。");
        beginningDialog.Add("稻荷神：等等就像這樣。嘿呀，直接打爆他。");
        beginningDialog.Add("稻荷神：等等就像這樣。嘿呀，直接打爆他。");
        beginningDialog.Add("稻荷神：我會幫你壓制他的力量。");
        beginningDialog.Add("稻荷神：你就當作練練手吧。");
        beginningDialog.Add("稻荷神：............");
        beginningDialog.Add("稻荷神：你說是像怎樣?");
        beginningDialog.Add("稻荷神：蛤?");
        beginningDialog.Add("稻荷神：敢頂嘴。");
        beginningDialog.Add("稻荷神：回來給你好看。");

        Time.timeScale = 0;
        dialogText.text = beginningDialog[0];
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
        if (dialogState < beginningDialog.Count)
        { 
            dialogText.text = beginningDialog[dialogState];
        }
        if (dialogState >= beginningDialog.Count)
        {
            dialogImage.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
