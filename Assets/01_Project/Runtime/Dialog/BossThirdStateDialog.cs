using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossThirdStateDialog : MonoBehaviour
{
    public List<string> bossSecondStateDialog;

    public int dialogState = 1;
    public Text dialogText;
    public GameObject dialogImage;
    void Start()
    {
        bossSecondStateDialog.Add("稻荷神：小心點別被他的雷刑擊中了。");
        bossSecondStateDialog.Add("稻荷神：她為了使出此技能已經不能用無敵護罩了。");
        bossSecondStateDialog.Add("稻荷神：直接將他擊敗吧。");

        Time.timeScale = 0;
        dialogText.text = bossSecondStateDialog[0];
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
        if (dialogState < bossSecondStateDialog.Count)
        {
            dialogText.text = bossSecondStateDialog[dialogState];
        }
        if (dialogState >= bossSecondStateDialog.Count)
        {
            dialogImage.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
