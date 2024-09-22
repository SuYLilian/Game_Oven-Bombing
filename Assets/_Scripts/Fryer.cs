using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fryer : MonoBehaviour
{
    public bool isEmpty;
    public int itemNum;
    public bool isBake, isSoonToBurn, isMidTime;

    public float timeCount_bake, timeCount_soonToBurn, timeCount_midTime;
    public float bakeTime, soonToBurnTime, midTime;
    public Image fryerEnergyBar_image;
    public Sprite energyBar_burn, energyBar_bake;
    public GameObject fryerEnergyBar;
    public GameObject smokeGroup, whiteSmoke, blackSmoke;
    //public int stateNum; //0ÁÙ¨S¼ô¡A1¼ô¤F¡A2¯NµJ
    public AudioSource audioSource;
    public AudioClip burning;
    private void Awake()
    {
        timeCount_bake = 0;
        timeCount_soonToBurn = 0;
        timeCount_midTime = 0;
    }
    private void Update()
    {
        if(!isEmpty)
        {
            if(isBake == true)
            {
                timeCount_bake += Time.deltaTime;
                fryerEnergyBar_image.fillAmount = timeCount_bake / bakeTime;
                if(timeCount_bake >= bakeTime)
                {
                    fryerEnergyBar.SetActive(false);
                    timeCount_bake = 0;
                    isBake = false;
                    isMidTime = true;
                    itemNum = 8;
                    fryerEnergyBar_image.sprite = energyBar_burn;
                    fryerEnergyBar_image.fillAmount = 0;
                   // stateNum = 1;
                    Debug.Log("¼ô¤F(¬µÁç)");
                }
            }
            else if(isMidTime == true)
            {
                timeCount_midTime += Time.deltaTime;
                if(timeCount_midTime >=midTime)
                {
                    fryerEnergyBar.SetActive(true);
                    timeCount_midTime = 0;
                    isMidTime = false;
                    isSoonToBurn = true;
                    audioSource.PlayOneShot(burning);
                }
            }
            else if(isSoonToBurn == true )
            {
                timeCount_soonToBurn += Time.deltaTime;
                fryerEnergyBar_image.fillAmount = timeCount_soonToBurn / soonToBurnTime;
                if(timeCount_soonToBurn >= soonToBurnTime)
                {
                    whiteSmoke.SetActive(false);
                    blackSmoke.SetActive(true);
                    fryerEnergyBar.SetActive(false);
                    timeCount_soonToBurn = 0;
                    isSoonToBurn = false;
                    itemNum = 18;
                   // stateNum = 2;
                    Debug.Log("µJ¤F(¬µÁç)");
                }
            }
        }
        
    }

    public void ResetValue()
    {
        timeCount_bake = 0;
        timeCount_midTime = 0;
        timeCount_soonToBurn = 0;
        //stateNum = 0;
        isBake = false;
        isMidTime = false;
        isSoonToBurn = false;
        fryerEnergyBar.SetActive(false);
        fryerEnergyBar_image.sprite = energyBar_bake;
        fryerEnergyBar_image.fillAmount = 0;
    }
}
