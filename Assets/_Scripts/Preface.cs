using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Preface : MonoBehaviour
{
    //public float duration;
    //float count;
    public float fadeSpeed;
    int num = 0;
    public Image[] prefaceImage;
    bool isFadeIn = true, isFadeOut = false;
    public GameObject nextButton, goButton;
    private void Update()
    {
        if (isFadeIn)
        {
            prefaceImage[num].color += new Color(0, 0, 0, fadeSpeed * Time.deltaTime);
            if (prefaceImage[num].color.a >= 1)
            {
                prefaceImage[num].color = new Color(1, 1, 1, 1);
                isFadeIn = false;
                if(num != 3)
                {
                    nextButton.SetActive(true);
                }
                else
                {
                    goButton.SetActive(true);
                }
            }
        }
        if (isFadeOut)
        {
            prefaceImage[num-1].color -= new Color(0, 0, 0, fadeSpeed * Time.deltaTime);
            if(prefaceImage[num-1].color.a<=0)
            {
                prefaceImage[num - 1].color = new Color(1, 1, 1, 0);
                isFadeOut = false;
                isFadeIn = true;
            }
        }
    }

    public void ClickNextButton()
    {
        FindObjectOfType<BGM>().PlayAudioClip(FindObjectOfType<BGM>().clips[0]);
        if (num < 3)
        {
            nextButton.SetActive(false);
            num++;
            isFadeOut = true;
        }
        /*else if(num == 3)
        {
            SceneManager.LoadScene("Game_0");
        }*/

    }
    public void ClickGoButton()
    {
        FindObjectOfType<BGM>().PlayAudioClip(FindObjectOfType<BGM>().clips[0]);
        SceneManager.LoadScene("Game_0");
    }
    /*public void SwitchScene()
    {
        SceneManager.LoadScene("Game_0");
    }*/
}
