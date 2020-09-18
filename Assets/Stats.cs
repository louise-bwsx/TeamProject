using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Stats : MonoBehaviour
{
    int baseValue;

    List<float> modifiers = new List<float>();
    public int GetValue()
    {
        float FinalValue = baseValue;
        modifiers.ForEach(x => FinalValue += x);
        return (int)FinalValue;
    }
    public void AddModifiers(float modifier)
    {
        if (modifier != 0)
        {
            modifiers.Add(modifier);
        }
    }
    public void RemoveModifiers(float modifier)
    {
        if (modifier != 0)
        {
            modifiers.Remove(modifier);
        }
    }
}
