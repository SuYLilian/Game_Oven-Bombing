using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManagerment : MonoBehaviour
{
    //public int breadNum_8, breadNum_9, breadNum_10, breadNum_11, breadNum_12, breadNum_13, breadNum_14, breadNum_15, breadNum_16, breadNum_17;
    public int[] levelBreadType;
    public float[] levelBreadNum;
    public float[] makenBreadNum;
    public Image[] energyBar;
    public float count, playTime;
    public float cut_roll_time;
    public Image countImage;
    public Sprite redCircle, orangeCircle;
    bool isTimeWillEnd = false, isTimeHalf=false;
    public GameObject gameLosePanel;

    public float mouseResidenceTime;
    public Vector2 mouse1_showDuration, mouse2_showDuration;
    public float mouse1_showDuration_count, mouse2_showDuration_count;
    bool canShowMouse = false;
    public PlateArea[] plateArea;
    public DisplayArea displayArea;
    public GameObject mouse2_prefab;
    public GameObject[] mouse1_prefab;
    public GameObject mouse1_showArea;

    public Image levelLable, instructions;
    public int levelNum;

    public bool isGaming = false;

    public GameObject[] displayAreas;
    public GameObject[] finishBread;
    //public GameObject totem;

    public int menuOrder_level1;
    public GameObject menuGroup;
    public GameObject baguetteMenu_prefab, croissantMenu_prefab;

    public static bool isFirstPlay = true;

    public Player[] players;

    public GameObject totemArrow_level_1, totemArrow_level_2;

    public AudioSource audioSource;
    private void Awake()
    {
        mouse1_showDuration_count = Random.Range(mouse1_showDuration.x, mouse1_showDuration.y);
        mouse2_showDuration_count = Random.Range(mouse2_showDuration.x, mouse2_showDuration.y);
        canShowMouse = true;

        //Time.timeScale = 0;
        count = playTime;

    }
    private void Update()
    {
        if(isGaming == true)
        {
            count -= Time.deltaTime;
            countImage.fillAmount = count / playTime;
            if (isTimeHalf == false && count <= playTime / 2)
            {
                countImage.sprite = orangeCircle;
                isTimeHalf = true;
            }
            else if (isTimeWillEnd == false && count <= playTime / 4)
            {
                audioSource.Play();
                countImage.sprite = redCircle;
                isTimeWillEnd = true;
            }
            if (count <= Time.deltaTime)
            {
                //Time.timeScale = 0;
                audioSource.Stop();
                players[0].rb.velocity = Vector3.zero;
                players[0].rb.angularVelocity = Vector3.zero;
                players[1].rb.velocity = Vector3.zero;
                players[1].rb.angularVelocity = Vector3.zero;
                isGaming = false;
                gameLosePanel.SetActive(true);
            }
            if (canShowMouse == true)
            {
                mouse1_showDuration_count -= Time.deltaTime;
                mouse2_showDuration_count -= Time.deltaTime;
                if (mouse2_showDuration_count <= 0)
                {
                    mouse2_showDuration_count = Random.Range(mouse2_showDuration.x, mouse2_showDuration.y);
                    int r = Random.Range(0, plateArea.Length);
                    while (plateArea[r].haveMouse == true)
                    {
                        r = Random.Range(0, plateArea.Length);
                    }
                    if(r == plateArea.Length)
                    {
                        Instantiate(mouse2_prefab, displayArea.mouseArea.transform);
                        displayArea.haveMouse = true;
                    }
                    else
                    {
                        Instantiate(mouse2_prefab, plateArea[r].mouseArea.transform);
                        plateArea[r].haveMouse = true;
                    }
                }
                if (mouse1_showDuration_count <= 0)
                {
                    mouse1_showDuration_count = Random.Range(mouse1_showDuration.x, mouse1_showDuration.y);
                    int r = Random.Range(0, mouse1_prefab.Length);

                    Instantiate(mouse1_prefab[r], mouse1_showArea.transform);
                    //plateArea[r].haveMouse = true;
                }
            }
        }        
    }

    public bool CanFinishMenu(int itemNum)
    {
        if(menuOrder_level1 == 0 && itemNum == 15)
        {
            menuGroup.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = 1;
            Instantiate(finishBread[0], displayAreas[0].transform);

            StartCoroutine(CreatCroissantMenu());
            menuGroup.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
            //Destroy(menuGroup.transform.GetChild(0).gameObject);
            //menuGroup.GetComponent<GridLayoutGroup>().enabled = true;
            //Instantiate(croissantMenu_prefab, menuGroup.transform);
            menuOrder_level1++;
            return true;
        }
        else if(menuOrder_level1 == 1 && itemNum == 11)
        {
            menuGroup.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = 1;
            Instantiate(finishBread[1], displayAreas[1].transform);
            StartCoroutine(CreatCroissantBaguetteMenu());
            menuGroup.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
            //Destroy(menuGroup.transform.GetChild(0).gameObject);
            //Instantiate(croissantMenu_prefab, menuGroup.transform);
            //Instantiate(baguetteMenu_prefab, menuGroup.transform);
            menuOrder_level1++;
            return true;
        }
        else if(menuOrder_level1 == 2 && (itemNum == 11 || itemNum == 9))
        {
            for(int i=0;i<menuGroup.transform.childCount;i++)
            {
                if(itemNum == int.Parse(menuGroup.transform.GetChild(i).gameObject.tag))
                {
                    menuGroup.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = 1;
                    if(itemNum == 11)
                    {
                        Instantiate(finishBread[2], displayAreas[2].transform);
                    }
                    else if(itemNum == 9)
                    {
                        Instantiate(finishBread[3], displayAreas[3].transform);
                    }
                    menuGroup.transform.GetChild(i).GetChild(2).gameObject.SetActive(true);
                    if(menuGroup.transform.childCount<=1)
                    {
                        totemArrow_level_1.SetActive(true);
                    }
                    //Destroy(menuGroup.transform.GetChild(i).gameObject);
                    break;
                }
            }          
            return true;
        }
        return false;
    }

    public bool CanIncreaseEnerage(int itemNum)
    {
        int flag = 1;
        int recordParameter=0;
        for (int i=0;i<levelBreadType.Length;i++)
        {
            if(itemNum == levelBreadType[i])
            {
                recordParameter = i;
                flag = 2;
                break;
            }
        }
        if(flag == 2)
        {
            if(makenBreadNum[recordParameter] < levelBreadNum[recordParameter])
            {
                makenBreadNum[recordParameter] += 1;
                energyBar[recordParameter].fillAmount = makenBreadNum[recordParameter] / levelBreadNum[recordParameter];

                for(int i=0;i<displayAreas.Length;i++)
                {
                    if(levelBreadType[recordParameter] == int.Parse(displayAreas[i].tag) && displayAreas[i].transform.childCount<=0)
                    {
                        Instantiate(finishBread[i], displayAreas[i].transform);
                        break;
                    }
                }
                if(makenBreadNum[recordParameter] / levelBreadNum[recordParameter]>=1)
                {
                    energyBar[recordParameter].gameObject.transform.parent.parent.GetChild(2).gameObject.SetActive(true);
                }
                if(menuGroup.transform.childCount<=1 && energyBar[recordParameter].fillAmount >= 1)
                {
                    totemArrow_level_2.SetActive(true);
                }
                return true;
            }
        }
        else
        {
            return false;
        }

        return false;
    }
    public bool IsFinish()
    {
        int flag = 1;
        for(int i=0;i<levelBreadType.Length;i++)
        {
            if(makenBreadNum[i] != levelBreadNum[i])
            {
                flag = 2;
                break;
            }
        }
        if(flag == 2)
        {
            return false;
        }
        else if(flag == 1)
        {
            return true;
        }

        return false;
    }   

    public IEnumerator CreatCroissantMenu()
    {
        yield return new WaitForSeconds(1.5f);
        Instantiate(croissantMenu_prefab, menuGroup.transform);
    }
    public IEnumerator CreatCroissantBaguetteMenu()
    {
        yield return new WaitForSeconds(1.5f);
        Instantiate(croissantMenu_prefab, menuGroup.transform);
        Instantiate(baguetteMenu_prefab, menuGroup.transform);
    }
}
