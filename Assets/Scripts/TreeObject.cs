using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeObject : MonoBehaviour
{
    private int level = 1;

    public int GetLevel()
    {
        return level;
    }
    public void SetLevel(int userSelectedLevel)
    {
        level = userSelectedLevel;
    }
    //private int exp = 0;

    void increaseExp()
    {
    }

    void increaseLevel()
    {
        //level += 1;
    }

    void Update()
    {
    }
}
