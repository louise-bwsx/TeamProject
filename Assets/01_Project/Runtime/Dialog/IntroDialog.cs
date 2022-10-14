using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroDialog : MonoBehaviour
{
    [SerializeField] private List<string> introDialog;
    [SerializeField] private TMP_Text dialogText;
    [SerializeField] private GameObject dialog;
    [SerializeField] private Button nextDialogBtn;
    private GameMenuController gameMenu;
    private int dialogState;

    private void Awake()
    {
        gameMenu = GetComponent<GameMenuController>();
    }

    private void Start()
    {
        nextDialogBtn.onClick.AddListener(DialogChange);
        AudioManager.Inst.PlayBGM("GameSceneIntro");
        //TODO: 之後想統一在SceneManager.OnSceneLoad呼叫
        gameMenu.OpenMenu("IntroDialog");
        Time.timeScale = 0;
        dialogText.text = introDialog[0];
    }

    private void DialogChange()
    {
        dialogState++;
        if (dialogState >= introDialog.Count)
        {
            dialog.SetActive(false);
            Time.timeScale = 1;
            AudioManager.Inst.PlayBGM("AfterGameSceneIntro");
            dialogState = 0;
            return;
        }
        dialogText.text = introDialog[dialogState];
    }
}