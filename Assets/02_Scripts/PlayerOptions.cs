using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOptions : MonoBehaviour
{
    public GameObject playerhealthbar;
    public GameObject monsterhealthbar;
    bool playerswtitch = false;
    public void SetPlayerHealthActive()
    {
        if (playerswtitch == true)
        {
            playerhealthbar.SetActive(playerswtitch);
            playerswtitch = false;
        }
        else
        {
            playerhealthbar.SetActive(playerswtitch);
            playerswtitch = true;
        }
    }
    public void SetMonsterHealthActive()
    {
        if (playerswtitch == true)
        {
            monsterhealthbar.SetActive(playerswtitch);
            playerswtitch = false;
        }
        else
        {
            monsterhealthbar.SetActive(playerswtitch);
            playerswtitch = true;
        }
    }
}
