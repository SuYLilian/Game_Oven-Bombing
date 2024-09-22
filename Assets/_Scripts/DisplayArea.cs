using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayArea : MonoBehaviour
{
    //public bool isEmpty;
    //public int itemNum;
    public GameObject area;

    public GameObject mouseArea;
    public bool haveMouse = false;

    public float mouseResidenceTime, mouseResidenceTime_count;
    public LevelManagerment levelManagerment;

    //public bool isDisplayArea;

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
            if (mouseResidenceTime_count <= 0)
            {
                haveMouse = false;
                Destroy(mouseArea.transform.GetChild(0).gameObject);
                for(int i=0;i<area.transform.childCount;i++)
                {
                    if(area.transform.GetChild(i).childCount > 0)
                    {
                        Destroy(area.transform.GetChild(i).GetChild(0).gameObject);
                        break;
                    }
                }

                mouseResidenceTime_count = mouseResidenceTime;
            }
        }
    }
}
