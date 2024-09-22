using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateArea : MonoBehaviour
{
    public bool isEmpty;
    public int itemNum;
    public GameObject area;

    public GameObject mouseArea;
    public bool haveMouse = false;

    public float mouseResidenceTime, mouseResidenceTime_count;
    public LevelManagerment levelManagerment;
    public GameObject exclamation;

    float timeLeft_exclamationShow = 4;
    public bool isShow_exclamation = false;
    public bool isDisplayArea;

    private void Awake()
    {
        levelManagerment = FindObjectOfType<LevelManagerment>();
        mouseResidenceTime = levelManagerment.mouseResidenceTime;
        mouseResidenceTime_count = mouseResidenceTime;
    }

    private void Update()
    {
        if (haveMouse == true)
        {
            mouseResidenceTime_count -= Time.deltaTime;
            if(mouseResidenceTime_count <= timeLeft_exclamationShow && isShow_exclamation == false)
            {
                isShow_exclamation = true;
                exclamation.SetActive(true);
            }
            else if (mouseResidenceTime_count <= 0)
            {
                haveMouse = false;
                isShow_exclamation = false;
                exclamation.SetActive(false);
                Destroy(mouseArea.transform.GetChild(1).gameObject);
                for(int i=0;i<area.transform.childCount;i++)
                {
                    Destroy(area.transform.GetChild(i).gameObject);
                }
                isEmpty = true;
                /*int r = Random.Range(0, levelManagerment.makenBreadNum.Length);
                while (levelManagerment.makenBreadNum[r] <= 0)
                {
                    r = Random.Range(0, levelManagerment.makenBreadNum.Length);
                }
                levelManagerment.makenBreadNum[r]--;
                levelManagerment.energyBar[r].fillAmount = levelManagerment.makenBreadNum[r] / levelManagerment.levelBreadNum[r];*/

                mouseResidenceTime_count = mouseResidenceTime;
            }
        }
    }
}
