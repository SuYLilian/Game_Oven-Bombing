using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bake : MonoBehaviour
{
    public bool isEmpty;
    public int itemNum;
    public bool isBake, isSoonToBurn, isMidTime;
    public bool haveFirewood = false;
    public int fireWoodNum;
    public Sprite[] firewoodEnergyBar_color;
    public float timeCount_bake, timeCount_soonToBurn, timeCount_midTime, count_firewoodReduceTime;
    public float bakeTime, soonToBurnTime, midTime, firewoodReduceTime;
    public Image firewoodEnergyBar, bakeEnergyBar_image;
    public GameObject bakeEnergyBar;
    public Sprite energyBar_burn, energyBar_bake;
    public int stateNum; //0ÁÙ¨S¼ô¡A1¼ô¤F¡A2¯NµJ
    public GameObject fire;
    public GameObject blackSmoke, whiteSmoke;
    public GameObject smokeGroup;

    public AudioSource audioSource;
    public AudioClip burning;
    private void Awake()
    {
        timeCount_bake = 0;
        timeCount_soonToBurn = 0;
        timeCount_midTime = 0;
        count_firewoodReduceTime = 0;
    }
    private void Update()
    {
        if (haveFirewood == true)
        {
            count_firewoodReduceTime += Time.deltaTime;
            if (count_firewoodReduceTime >= firewoodReduceTime)
            {
                fireWoodNum--;
                firewoodEnergyBar.sprite = firewoodEnergyBar_color[fireWoodNum];
                count_firewoodReduceTime = 0;
                if (fireWoodNum <= 0)
                {
                    smokeGroup.SetActive(false);
                    haveFirewood = false;
                    fire.SetActive(false);
                }
            }
            if (!isEmpty)
            {
                if (isBake == true)
                {
                    timeCount_bake += Time.deltaTime;
                    bakeEnergyBar_image.fillAmount = timeCount_bake / bakeTime;
                    if (timeCount_bake >= bakeTime)
                    {
                        bakeEnergyBar.SetActive(false);
                        bakeEnergyBar_image.sprite = energyBar_burn;
                        bakeEnergyBar_image.fillAmount = 0;
                        timeCount_bake = 0;
                        isBake = false;
                        isMidTime = true;
                        //itemNum = 8;
                        stateNum = 1;
                        itemNum = JudgeItemNum(itemNum);
                        Debug.Log("¼ô¤F(¯N½c)");
                    }
                }
                else if (isMidTime == true)
                {
                    timeCount_midTime += Time.deltaTime;
                    if (timeCount_midTime >= midTime)
                    {
                        bakeEnergyBar.SetActive(true);

                        timeCount_midTime = 0;
                        isMidTime = false;
                        isSoonToBurn = true;
                        audioSource.PlayOneShot(burning);

                    }
                }
                else if (isSoonToBurn == true)
                {
                    timeCount_soonToBurn += Time.deltaTime;
                    bakeEnergyBar_image.fillAmount = timeCount_soonToBurn / soonToBurnTime;
                    if (timeCount_soonToBurn >= soonToBurnTime)
                    {
                        whiteSmoke.SetActive(false);
                        blackSmoke.SetActive(true);
                        bakeEnergyBar.SetActive(false);
                        timeCount_soonToBurn = 0;
                        isSoonToBurn = false;
                        //itemNum = 18;
                        stateNum = 2;
                        itemNum = JudgeItemNum(itemNum);
                        Debug.Log("µJ¤F(¯N½c)");
                    }
                }
            }

        }


    }

    int JudgeItemNum(int _itemNum)
    {
        int t = _itemNum;
        if (stateNum == 1)
        {
            switch (_itemNum)
            {
                case 6:
                    t = 9;
                    break;
                case 5:
                    t = 10;
                    break;
                case 7:
                    t = 11;
                    break;
            }
        }
        else if (stateNum == 2)
        {
            switch (_itemNum)
            {
                case 9:
                    t = 19;
                    break;
                case 10:
                    t = 20;
                    break;
                case 11:
                    t = 21;
                    break;
            }
        }

        return t;

    }

    public void ResetValue()
    {
        timeCount_bake = 0;
        timeCount_midTime = 0;
        timeCount_soonToBurn = 0;
        stateNum = 0;
        isBake = false;
        isMidTime = false;
        isSoonToBurn = false;
        bakeEnergyBar.SetActive(false);
        bakeEnergyBar_image.sprite = energyBar_bake;
        bakeEnergyBar_image.fillAmount = 0;
    }
}
