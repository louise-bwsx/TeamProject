using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogueManager : MonoSingleton<DialogueManager>, ISave
{
    [SerializeField] private DialogController[] dialogues;
    private List<string> watchedDialogue = new List<string>();

    private void Awake()
    {
        SaveManager.Inst.ISaves.Add(this);
        Load();
    }

    private void Start()
    {
        dialogues[0].OnDialogueFinish.AddListener(IntroDialogueFinish);
        dialogues[4].OnDialogueFinish.AddListener(BossDieDialogueFinish);
    }

    public void ShowDialogue(string dialogueName)
    {
        DialogController dialogue = dialogues.FirstOrDefault(dialogue => dialogue.name == dialogueName);
        bool isWatched = watchedDialogue.Any(name => name == dialogueName);
        if (isWatched)
        {
            return;
        }
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
        Time.timeScale = 0;
        GameStateManager.Inst.ChangState(GameState.GameEnd);
        UIManager.Inst.OpenMenu("Credit");
    }

    public void SetDialogueFinish(string dialogugName)
    {
        watchedDialogue.Add(dialogugName);
    }

    public void Save(GameSaveData gameSave)
    {
        gameSave.watchedDialogue = watchedDialogue;
    }

    public void Load()
    {
        GameSaveData gameSave = SaveManager.Inst.GetGameSave();
        watchedDialogue = gameSave.watchedDialogue;
    }
}