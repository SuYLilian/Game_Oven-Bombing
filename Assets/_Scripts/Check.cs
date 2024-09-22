using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : MonoBehaviour
{
    public GameObject menu;

    public void DestroyMenu()
    {
        Destroy(menu);
    }
}
