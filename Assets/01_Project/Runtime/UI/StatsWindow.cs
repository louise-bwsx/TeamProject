using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class StatsWindow : MonoBehaviour
{
    [SerializeField] private StatWindowItem[] statWindowItems;
    [SerializeField] private string[] statNames;
    [SerializeField] private int[] statLevels;
    [SerializeField] private TMP_Text dustText;

    private void Start()
    {
        Initialize();
    }


    [Button]
    private void Initialize()
    {
        for (int i = 0; i < statWindowItems.Length; i++)
        {
            statWindowItems[i].Initialize(statNames[i], statLevels[i]);
        }
    }
}