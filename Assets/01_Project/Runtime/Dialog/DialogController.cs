using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    [SerializeField] private List<string> dialog;
    [SerializeField] private TMP_Text dialogText;
    [SerializeField] private GameObject dialogObject;
    [SerializeField] private Button nextDialogBtn;
    [SerializeField] private string playBGMName;
    [SerializeField] private GameMenuController gameMenu;
    [SerializeField] private UnityEvent afterFinishThisDialog;
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
            if (playBGMName != null)
            {
                AudioManager.Inst.PlayBGM(playBGMName);
            }
            dialogState = 0;
            Time.timeScale = 1;
            GameStateManager.Inst.ChangState(GameState.Gaming);
            afterFinishThisDialog.Invoke();
            dialogObject.SetActive(false);
            return;
        }
        dialogText.text = dialog[dialogState];
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
}