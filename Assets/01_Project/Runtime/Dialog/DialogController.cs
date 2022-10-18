﻿using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    [SerializeField] private List<string> dialog;
    [SerializeField] private TMP_Text dialogText;
    [SerializeField] private GameObject dialogObject;
    [SerializeField] private Button nextDialogBtn;
    [SerializeField] private string afterFinishThisDialogPlayBGMName;
    [SerializeField] private GameMenuController gameMenu;
    [Header("BossDieDialog專用")]
    [SerializeField] private Image avatarImage;
    private int dialogState;

    private void OnEnable()
    {
        GameStateManager.Inst.ChangState(GameState.Dialog);
        Time.timeScale = 0;
        dialogText.text = dialog[0];
    }

    private void Start()
    {
        nextDialogBtn.onClick.AddListener(DialogChange);
    }

    private void DialogChange()
    {
        dialogState++;
        if (dialogState >= dialog.Count)
        {
            if (afterFinishThisDialogPlayBGMName != "")
            {
                AudioManager.Inst.PlayBGM(afterFinishThisDialogPlayBGMName);
            }
            dialogState = 0;
            Time.timeScale = 1;
            GameStateManager.Inst.ChangState(GameState.Gaming);
            AfterIntroDialog();
            dialogObject.SetActive(false);
            return;
        }
        ChangeAvatar();
        dialogText.text = dialog[dialogState];
    }

    //BossDieDialog專用
    private void ChangeAvatar()
    {
        if (gameObject.name != "BossDialog")
        {
            return;
        }
        if (dialogState == 3 || dialogState == 6 || dialogState == 7)
        {
            avatarImage.enabled = true;
        }
        else
        {
            avatarImage.enabled = false;
        }
    }

    //IntroDialog專用
    private void AfterIntroDialog()
    {
        if (gameObject.name != "IntroDialog")
        {
            return;
        }
        gameMenu.OpenMenu("GodTalkDialog");
    }

    //GameSceneIntro:
    //"此人類的貪嗔癡等，負面能量在世間中形成一股渾沌"
    //"此種渾沌會令其所接觸的人事物及具侵略性"
    //"而在神社中長期接觸百姓的神明首先被影響"
    //"失去山野中各路神明的調和使得災害與飢荒越演越烈"
    //"於是與山林息息相關的稻荷大神派出使者祓除渾沌"

    //AfterGameSceneIntro:
    //"稻荷神：快到了，就在裡面。"
    //"稻荷神：等等就像這樣。嘿呀，直接打爆他。"
    //"稻荷神：我會幫你壓制他的力量。"
    //"稻荷神：你就當作練練手吧。"
    //"稻荷神：............"
    //"稻荷神：你說是像怎樣?"
    //"稻荷神：蛤?"
    //"稻荷神：敢頂嘴。"
    //"稻荷神：回來給你好看。"

    //BossSecondState:
    //"稻荷神：他現在擁有特殊結界。"
    //"稻荷神：使用組合技吧。"
    //"稻荷神：往毒裡丟入火或是往風裡丟入火試試看。"

    //BossThirdStateDialog
    //"稻荷神：小心點別被他的雷刑擊中了。"
    //"稻荷神：她為了使出此技能已經不能用無敵護罩了。"
    //"稻荷神：直接將他擊敗吧。"

    //BossDieDialog
    //"天王：啊!"
    //"天王：(看了看自己)"
    //"天王：謝謝你的幫忙。"
    //"稻荷神：好了，你先回來一趟吧!"
    //"天王：诶?你給我使者令牌幹嘛?"
    //"(狐狸轉身離去)"
    //"稻荷神：你要去哪裡?"
    //"稻荷神：給我回來!"
    //"遊戲結束"
    //"製作人員名單畫面顯現"//這句應該不用加吧?? 不知道為什麼原本有

    //Skill
    //毒
    //範圍遠程技能，使烈焰爆發
    //水
    //強力的水彈可推開敵人
    //風
    //範圍遠程技能，使烈焰燎原
    //土
    //厚實的土牆可阻擋敵人
    //火
    //單體遠程技能，能夠使毒爆炸，令風燃燒
}