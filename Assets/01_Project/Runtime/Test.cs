using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

public class Test : MonoBehaviour
{
    [SerializeField] private TMP_FontAsset msjh;
    [SerializeField] private TMP_Text[] allTexts;
    [SerializeField] private List<TMP_Text> missingText = new List<TMP_Text>();

    [Button]
    private void FindMissingTMP_Text()
    {
        missingText.Clear();
        allTexts = FindObjectsOfType<TMP_Text>(true);
        for (int i = 0; i < allTexts.Length; i++)
        {
            if (allTexts[i].font == null)
            {
                missingText.Add(allTexts[i]);
            }
        }
    }

    [Button]
    private void ChangeFontAsset()
    {
        for (int i = 0; i < missingText.Count; i++)
        {
            missingText[i].font = msjh;
        }
    }
}
