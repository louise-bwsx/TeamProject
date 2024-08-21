using System.Linq;
using UnityEngine;

public class DialogueManager : MonoSingleton<DialogueManager>
{
    [SerializeField] private DialogController[] dialogues;

    private void Start()
    {
        dialogues[0].OnDialogueFinish.AddListener(IntroDialogueFinish);
        dialogues[4].OnDialogueFinish.AddListener(BossDieDialogueFinish);
    }

    public void ShowDialogue(string dialogueName)
    {
        DialogController dialogue = dialogues.FirstOrDefault(dialogue => dialogue.name == dialogueName);
        if (!dialogue)
        {
            Debug.LogError($"ß‰§£®ÏDialogue: {dialogueName}");
            return;
        }
        GameStateManager.Inst.ChangState(GameState.Dialogue);
        dialogue.gameObject.SetActive(true);
        dialogue.StartDialogue();

        Time.timeScale = 0;
    }

    private void IntroDialogueFinish()
    {
        AudioManager.Inst.PlayBGM("AfterGameSceneIntro");
        ShowDialogue("GodTalkDialogue");
    }

    private void BossDieDialogueFinish()
    {
        AudioManager.Inst.PlayBGM("AfterBossFight");
    }
}