using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStart : MonoBehaviour
{
    LevelManagerment levelManagerment;
    private void Awake()
    {
        
        levelManagerment = FindObjectOfType<LevelManagerment>();
        if (LevelManagerment.isFirstPlay == false && levelManagerment.levelNum == 0)
        {
            gameObject.SetActive(false);
            levelManagerment.isGaming = true;
            //Time.timeScale = 1;
        }
    }

    public void StarGame()
    {
        levelManagerment.isGaming = true;
        LevelManagerment.isFirstPlay = false;
    }
}
