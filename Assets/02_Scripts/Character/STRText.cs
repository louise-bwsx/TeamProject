using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class STRText : MonoBehaviour
{
    public CharacterBase characterBase;
    void Update()
    {
        GetComponent<Text>().text = characterBase.STR + "\n" ;
    }

}
