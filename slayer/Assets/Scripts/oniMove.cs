﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class oniMove : MonoBehaviour
{
    public bool isJumping;
    public bool canGo;
    public int hp = 2;
    public int oniIndex;
    public float minX, maxX;
    private bool isStop = false;
    public GameObject effect;
    public GameObject headEffect;
    public float speed;
    
    private Animator anim;
    public float dmgDelay = 0;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (isJumping)
            transform.right = GetComponent<Rigidbody2D>().velocity;
        
        if(canGo)
        {
            if (!isStop)
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
        }

        if (!isStop)
        { 
            if (transform.position.x >= minX && transform.position.x <= maxX) 
            { 
                StartCoroutine(attack());
                isStop = true;
            }
        }

        if (dmgDelay < Time.deltaTime*10)
            dmgDelay += Time.deltaTime;
    }

    IEnumerator attack()
    {
        if (!isStop)
        {
            while (true)
            {
                if (oniIndex == 1)
                {
                    anim.Play("oni1Attack");
                    StartCoroutine(girl.instance.hitted(10));
                    yield return new WaitForSeconds(0.5f);
                    anim.Play("oni1Idle");
                    yield return new WaitForSeconds(1f);
                }
                else if(oniIndex==2)
                {
                    anim.Play("oni2Attack");
                    StartCoroutine(girl.instance.hitted(30));
                    yield return new WaitForSeconds(0.5f);
                    anim.Play("oni2Idle");
                    yield return new WaitForSeconds(2f);
                }
            }
        }
        else
        {
            yield return null;
        }
    }

    public void die(bool isHead)
    {
        if (dmgDelay >= 0.1f)
        { 
            Player.instance.AttackCor();
            if (isHead)
            {
                ScoreMgr.instance.headshot++;
                SoundManager.instance.head();
                if (oniIndex == 1)
                {
                    
                    ComboManager.instance.comboIniitailize();
                    ScoreMgr.instance.killedOni++;
                    ScoreMgr.instance.scoreUp(100, false);
                    Instantiate(headEffect, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
                else if (oniIndex == 2)
                {
                    hp -= 2;
                    if (hp <= 0)
                    {
                        ComboManager.instance.comboIniitailize();
                        ScoreMgr.instance.killedOni++;
                        ScoreMgr.instance.scoreUp(200, false);
                        Instantiate(headEffect, transform.position, Quaternion.identity);
                        Destroy(gameObject);
                    }
                }
            }
            else
            {
                SoundManager.instance.body();
                if (oniIndex == 1)
                {
                    ComboManager.instance.comboIniitailize();
                    ScoreMgr.instance.killedOni++;
                    ScoreMgr.instance.scoreUp(100, false);
                    Instantiate(effect, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
                else if (oniIndex == 2)
                {
                    hp--;
                    if (hp <= 0)
                    {
                        ComboManager.instance.comboIniitailize();
                        ScoreMgr.instance.killedOni++;
                        ScoreMgr.instance.scoreUp(200, false);
                        Instantiate(effect, transform.position, Quaternion.identity);
                        Destroy(gameObject);
                    }
                }
            }
            Player.instance.ComboText(isHead);
            dmgDelay = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!Player.instance.isattack)
            {
                if (oniIndex == 1)
                {
                    anim.Play("oni1Attack");
                    if (canGo)
                    {
                        if (other.transform.position.x < transform.position.x)
                            transform.localScale = new Vector3(1, 1, 1);
                        else if (other.transform.position.x > transform.position.x)
                            transform.localScale = new Vector3(-1, 1, 1);
                    }
                }
                else if(oniIndex==2)
                    anim.Play("oni2Attack");
            }
        }

        if (other.CompareTag("Ground"))
        {
            if (!canGo)
            {
                if (speed < 0)
                {
                    if(oniIndex==1) 
                        transform.localScale = new Vector3(1, 1, 1);
                    else if(oniIndex==2)
                        transform.localScale = new Vector3(5, 5, 1);
                }
                canGo = true;
                transform.eulerAngles = new Vector3(0, 0, 0);
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                if (oniIndex == 1)
                {
                    anim.Play("oni1Anim");
                    transform.Translate(0,   Random.Range(-0.1f,-0.05f), 0);
                }
                else if (oniIndex == 2)
                {
                    anim.Play("oni2_walk");
                    //transform.Translate(0,   Random.Range(0.5f,1f), 0);
                }
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
               
            }
        }
    }
}