using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOptions : MonoBehaviour
{
    public GameObject playerhealthbar;
    public GameObject[] monsterhealthbar;
    bool playerswtitch = false;
    bool monsterSwitch = false;

    void Start()
    {
        monsterhealthbar = GameObject.FindGameObjectsWithTag("EnemyCanvas");
    }

    public void SetPlayerHealthActive()
    {
        playerhealthbar.SetActive(playerswtitch);
        if (playerswtitch == true)
        {
            playerswtitch = false;
        }
        else
        {
            playerswtitch = true;
        }
    }
    public void SetMonsterHealthActive()
    {
        foreach (GameObject i in monsterhealthbar)
        {
            i.SetActive(playerswtitch);
        }
        if (monsterSwitch)
        {
            playerswtitch = false;
        }
        else
        {
            playerswtitch = true;
        }
    }
}
