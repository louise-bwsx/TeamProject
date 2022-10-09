using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSecondStateDialog : MonoBehaviour
{
    public List<string> bossSecondStateDialog;

    public int dialogState = 1;
    public Text dialogText;
    public GameObject dialogImage;
    void Start()
    {
        bossSecondStateDialog.Add("稻荷神：他現在擁有特殊結界。");
        bossSecondStateDialog.Add("稻荷神：使用組合技吧。");
        bossSecondStateDialog.Add("稻荷神：往毒裡丟入火或是往風裡丟入火試試看。");

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
