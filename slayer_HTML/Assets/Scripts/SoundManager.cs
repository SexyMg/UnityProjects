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
    public AudioClip DashSound;
    public AudioClip GrassSound;
    public AudioClip Skill1Sound;
    public AudioClip Skill2Sound;
    public AudioClip LockedSound;
    public AudioClip bestScoreSound;
    public static SoundManager instance;
    private AudioSource audio;
    
    public float savedBgm=1;
    public float savedBgs=1;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
       
        audio = GetComponent<AudioSource>();
    }

    public void Locked()
    {
        audio.PlayOneShot(LockedSound,savedBgs);
    }
    public void bestScore()
    {
        audio.PlayOneShot(bestScoreSound,savedBgs*3);
    }
    public void Skill1()
    {
        audio.PlayOneShot(Skill1Sound,savedBgs*3);
    }
    public void Skill2()
    {
        audio.PlayOneShot(Skill2Sound,savedBgs);
    }
    public void Grass()
    {
        audio.PlayOneShot(GrassSound,savedBgs*15);
    }
    public void Dash()
    {
        audio.PlayOneShot(DashSound,savedBgs*2);
    }
    public void SpiderAttack()
    {
        audio.PlayOneShot(SpiderAttackSound,savedBgs*5);
    }
    public void bgmValue(float v)
    {
        savedBgm = v;
    }

    public void bgsValue(float v)
    {
        savedBgs = v;
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