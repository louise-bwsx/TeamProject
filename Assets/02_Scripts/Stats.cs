using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Stats : MonoBehaviour
{
    float baseValue;
    float finalValue;
    List<float> modifiers = new List<float>();
    public int GetValue()
    {
        if (baseValue != 0)
        { 
            finalValue = baseValue;
        }
        modifiers.ForEach(x => finalValue += x);
        return (int)finalValue;
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
