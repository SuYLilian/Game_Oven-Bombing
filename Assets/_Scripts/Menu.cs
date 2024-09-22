using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
 
    public void ClickStartButton(string sceneName)
    {
        FindObjectOfType<BGM>().PlayAudioClip(FindObjectOfType<BGM>().clips[0]);
        SceneManager.LoadScene(sceneName);
    }
}
