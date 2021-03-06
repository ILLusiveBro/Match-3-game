﻿using UnityEngine;

public class soundListener : MonoBehaviour {

    void Awake()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("soundStatus",0.5f);
        DontDestroyOnLoad(GameObject.Find("soundListener"));
    }

    public void SoundCountrol()
    {
        if (AudioListener.volume == 0.5f)
        {
            AudioListener.volume = 0;
            PlayerPrefs.SetFloat("soundStatus", AudioListener.volume);
        }           
        else
        {
            AudioListener.volume = 0.5f;
            PlayerPrefs.SetFloat("soundStatus", AudioListener.volume);
        }         
    }
}