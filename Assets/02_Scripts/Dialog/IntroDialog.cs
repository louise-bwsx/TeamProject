using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroDialog : MonoBehaviour
{
    public List<string> introDialog;

    public int dialogState = 1;
    public Text dialogText;
    public GameObject dialogImage;
    public AudioSource audioSource;
    public AudioClip panelClip;
    public AudioClip walkClip;
    void Start()
    {
        introDialog.Add("人類的貪、嗔、癡等，負面能量聚積在世間中形成一股感染");
        introDialog.Add("此種汙染會令其所接觸的人事物極具攻擊性");
        introDialog.Add("而在神社中長期接觸百姓的神明首先被影響");
        introDialog.Add("失去山野中各路神明的調和使得災害與飢荒越演越烈");
        introDialog.Add("守護山林田野，與人類息息相關的稻荷神一邊阻止感染的擴散;一邊派出使者祓除感染");

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
        if (dialogImage.activeSelf == true)
        {
            if (audioSource.clip != panelClip)
            {
                audioSource.Stop();
                audioSource.clip = panelClip;
                audioSource.Play();
            }
        }
        else if (dialogImage.activeSelf == false)
        {
            if (audioSource.clip != walkClip)
            {
                audioSource.Stop();
                audioSource.clip = walkClip;
                audioSource.Play();
            }
        }
    }
}
