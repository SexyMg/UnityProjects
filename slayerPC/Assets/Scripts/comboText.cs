﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class comboText : MonoBehaviour
{
    public float speed;
    public bool isHeadShot = false;
    private Text text;
    Color color;
    private void Start()
    {
        text = GetComponent<Text>();

        if (!isHeadShot)
        {
            if (FindObjectOfType<ComboManager>().comboCount >= 2)
                GetComponent<Text>().text = FindObjectOfType<ComboManager>().comboCount + " Combo";
            
            color.r = 255;
            color.g = 255;
            color.b = 0;
            color.a = 1;
        }
        else
        {
            color.r = 255;
            color.g = 0;
            color.b = 0;
            color.a = 1;
        }
    }

    public void initialize()
    {
        transform.position = FindObjectOfType<Player>().transform.position;
        transform.position=transform.position+new Vector3(Random.Range(-0.5f,0.5f),Random.Range(-0.5f,0.5f));
    }

    void Update()
    {

        if (color.a > 0)
        {
            color.a -= speed;
            text.color = color;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
