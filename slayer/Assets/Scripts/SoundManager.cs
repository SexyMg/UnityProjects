﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    public AudioClip tsuzumiL, tsuzumiM, tsuzumiR;
    public AudioClip hitSound;
    public AudioClip swingSound;
    public AudioClip bodySound;
    public AudioClip headSound;
    public AudioClip girlSound;
    public AudioClip selectSound;
    public AudioClip comboSound;
    public AudioClip scoreCountSound;
    public AudioClip healSound;
    public AudioClip knifeCoverSound;
    public AudioClip SpiderAttackSound;
    public static SoundManager instance;
    private AudioSource audio;
    
    private string bgmKey = "bgmKey";
    private string bgsKey = "bgsKey";
    public float savedBgm;
    public float savedBgs;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            savedBgm = PlayerPrefs.GetFloat(bgmKey,1f);
            savedBgs = PlayerPrefs.GetFloat(bgsKey, 1f);
        }
        else
        {
            Destroy(gameObject);
        }
       
        audio = GetComponent<AudioSource>();
    }

    public void SpiderAttack()
    {
        audio.PlayOneShot(SpiderAttackSound,savedBgs*5);
    }
    public void bgmValue(float v)
    {
        savedBgm = v;
        PlayerPrefs.SetFloat(bgmKey,savedBgm);
    }

    public void bgsValue(float v)
    {
        savedBgs = v;
        PlayerPrefs.SetFloat(bgsKey,savedBgs);
    }
    public void knifeCover()
    {
        audio.PlayOneShot(knifeCoverSound,savedBgs);
    }
    public void heal()
    {
        audio.PlayOneShot(healSound,savedBgs);
    }
    public void scoreCount()
    {
        audio.PlayOneShot(scoreCountSound,savedBgs);
    }
    public void tsuzumi(int v)
    {
        if(v==0)
            audio.PlayOneShot(tsuzumiL,savedBgs);
        else if(v==1)
            audio.PlayOneShot(tsuzumiM,savedBgs);
        else if(v==2)
            audio.PlayOneShot(tsuzumiR,savedBgs);
    }
    public void swing()
    {
        audio.PlayOneShot(swingSound,savedBgs);
    }

    public void body()
    {
        audio.PlayOneShot(bodySound,savedBgs);
    }

    public void head()
    {
        audio.PlayOneShot(headSound,savedBgs*0.5f);
    }

    public void hit()
    {
        audio.PlayOneShot(hitSound,savedBgs*0.5f);
    }

    public void girl()
    {
        audio.PlayOneShot(girlSound,savedBgs);
    }

    public void combo()
    {
        audio.PlayOneShot(comboSound,savedBgs);
    }

    public void select()
    {
        audio.PlayOneShot(selectSound,savedBgs);
    }
}
