using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public static BGM bgm;

    public AudioClip[] clips;

    public AudioSource audioSource;
    private void Awake()
    {
        if(bgm == null)
        {
            bgm = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayAudioClip(AudioClip _clip)
    {
        audioSource.PlayOneShot(_clip);
    }

}
