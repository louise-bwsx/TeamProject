using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogComponent : MonoBehaviour
{
    public List<string> introDialog;
    private string dialog1 = "人類的貪、嗔、癡等，負面能量聚積在世間中形成一股汙染";
    private string dialog2 = "此種汙染會令其所接觸的人事物極具攻擊性";
    private string dialog3 = "而在神社中長期接觸百姓的神明首先被影響";
    private string dialog4 = "失去山野中各路神明的調和使得災害與飢荒越演越烈";
    private string dialog5 = "守護山林田野，與人類息息相關的稻禾大神一邊阻擋污染的擴散另一邊派出手下使者";
    private string dialog6 = "試圖要祓除汙染";

    public List<string> 
    private string dialog7 = "稻禾：快到了，就在裡面。";
    private string dialog8 = "稻禾：等等就像這樣，嘿呀，直接打爆他。";
    private string dialog9 = "稻禾：我會幫你壓制他的力量。你就當作練練手吧。";
    private string dialog10 = "稻禾：............";
    private string dialog11 = "稻禾：你說是像怎樣?";
    private string dialog12 = "稻禾：蛤?";
    private string dialog13 = "稻禾：敢頂嘴?";
    private string dialog14 = "稻禾：回來給你好看。";

    private string dialog15 = "BOSS：啊!";
    private string dialog16 = "BOSS：(看了看自己)";
    private string dialog17 = "BOSS：謝謝你的幫忙。";
    private string dialog18 = "稻禾：好了，你先回來一趟吧!";
    private string dialog19 = "BOSS：诶?你給我使者令牌幹嘛?";
    private string dialog20 = "(狐狸轉身離去)";
    private string dialog21 = "稻禾：你要去哪裡?";
    private string dialog22 = "稻禾：給我回來!";
    private string dialog23 = "Fin";

    public int dialogState = 1;
    public Text dialogText;
    public GameObject dialogImage;
    void Start()
    {
        introDialog.Add(dialog1);
        introDialog.Add(dialog2);
        introDialog.Add(dialog3);
        introDialog.Add(dialog4);
        introDialog.Add(dialog5);
        introDialog.Add(dialog6);

        Time.timeScale = 0;
        dialogText.text = dialog1;
    }
    void Update()
    {
        if (dialogImage.activeSelf)
        {
            Time.timeScale = 0;
        }
        else if (!dialogImage.activeSelf)
        {
            Time.timeScale = 1;
        }

    }
    public void DialogChange()
    {
        dialogState++;
        Debug.Log(dialogState);
        if (dialogState < introDialog.Count)
        { 
            dialogText.text = introDialog[dialogState];
        }
        if (dialogState >= introDialog.Count)
        {
            dialogImage.SetActive(false);
        }
    }
}
