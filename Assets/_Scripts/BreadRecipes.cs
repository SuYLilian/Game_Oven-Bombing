using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadRecipes : MonoBehaviour
{
    public int itemNum;
    public GameObject[] bread;

    /*public BreadRecipes(int itemInHand)
    {
        itemNum = itemInHand;
    }*/

    public int TrashCan(int itemInHand)
    {
        itemNum = itemInHand;
        if(itemNum!=0)
        {
            Debug.Log("空手");
            itemNum = 0;
        }
        return itemNum;
    }
    public int DoughCabinet_Fried(int itemInHand)
    {
        itemNum = itemInHand;
        if(itemNum == 0)
        {
            Debug.Log("炸麵團");
            itemNum = 1;//手拿炸麵團
        }
        /*else
        {
            Debug.Log(ItemNumType());
        }*/
        return itemNum;
    }
    public int DoughCabinet_Bake(int itemInHand)
    {
        itemNum = itemInHand;
        if (itemNum == 0)
        {
            Debug.Log("烤麵團");
            itemNum = 2;//手拿烤麵團
        }
        else
        {
            Debug.Log(ItemNumType());
        }
        return itemNum;
    }
    /*public int FryerCabinet(int itemInHand)
    {
        itemNum = itemInHand;
        if (itemNum == 3)
        {
            Debug.Log("甜甜圈");
            itemNum = 8;
        }
        return itemNum;
    }*/
    public int BakeCabinet(int itemInHand)
    {
        itemNum = itemInHand;
        if (itemNum == 6)
        {
            Debug.Log("法棍");
            itemNum = 9;
        }
        else if (itemNum == 5)
        {
            Debug.Log("吐司");
            itemNum = 10;
        }
        else if (itemNum == 7)
        {
            Debug.Log("可頌");
            itemNum = 11;
        }
        return itemNum;
    }
    public int CuttingBoardCabinet(int itemInHand)
    {
        itemNum = itemInHand;
        if(itemNum==1)
        {
            Debug.Log("切好的麵包(炸的)");
            itemNum = 3;
        }
        else if (itemNum == 2)
        {
            Debug.Log("切好的麵包(烤的)");
            itemNum = 4;
        }
        else if (itemNum == 5)
        {
            Debug.Log("桿好切好的麵包(烤的)");
            itemNum = 7;
        }
        else if(itemNum == 10)
        {
            itemNum = 24;
        }
        return itemNum;
    }
    public int Rodding(int itemInHand)
    {
        itemNum = itemInHand;
        if(itemNum==2)
        {
            Debug.Log("桿好的麵包(烤的)");
            itemNum = 5;
        }
        else if(itemNum==4)
        {
            Debug.Log("切好桿好的麵包(烤的)");
            itemNum = 6;
        }
        return itemNum;
    }
    public int CocoSauce(int itemInHand)
    {
        itemNum = itemInHand;
        if (itemNum == 8)
        {
            Debug.Log("巧克力醬甜甜圈");
            itemNum = 12;
        }
        else if (itemNum == 10)
        {
            Debug.Log("巧克力醬吐司");
            itemNum = 13;
        }
        else if (itemNum == 11)
        {
            Debug.Log("巧克力醬可颂");
            itemNum = 14;
        }
        return itemNum;
    }
    public int StrawberrySauce(int itemInHand)
    {
        itemNum = itemInHand;
        if(itemNum == 24)
        {
            itemNum = 25;
        }
        return itemNum;
    }
    public int CocoRice(int itemInHand)
    {
        itemNum = itemInHand;

        if (itemNum == 12)
        {
            Debug.Log("巧克力醬+巧米甜甜圈");
            itemNum = 15;
        }
        else if (itemNum == 13)
        {
            Debug.Log("巧克力醬+巧米吐司");
            itemNum = 16;
        }
        else if (itemNum == 14)
        {
            Debug.Log("巧克力醬+巧米可颂");
            itemNum = 17;
        }
        return itemNum;
    }

    public int ItemNumType()
    {
        return 0;
    }
}
