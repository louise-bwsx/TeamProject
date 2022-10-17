using Sirenix.OdinInspector;
using UnityEngine;

public class SkillWindow : MonoBehaviour
{
    [SerializeField] private SkillWindowItem[] skillWindowItems;
    [SerializeField] private Sprite[] skillIcons;
    [SerializeField] private string[] skillNames;
    [SerializeField] private string[] skillInfo;
    [SerializeField] private int[] skillLevel;

    private void Start()
    {
        Initialize();
    }

    [Button]
    private void Initialize()
    {
        for (int i = 0; i < skillWindowItems.Length; i++)
        {
            skillWindowItems[i].Initialize(skillIcons[i], skillNames[i], skillInfo[i], skillLevel[i]);
        }
    }
}