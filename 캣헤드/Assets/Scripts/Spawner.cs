﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public float SpawnDelayUpValue;
    private bool canBossSpawn = true;
    public GameObject BossMonster;
    private GameManager gm;
    public GameObject monster;
    public float samlldelay,bigdelay;
    private bool btn = false;

    void Start()
    {
        if (gameObject.name == "LSpawner" || gameObject.name == "RSpawner" || gameObject.name == "LRSpawner"|| gameObject.name == "RSpawner2" || gameObject.name == "LSpawner2"
            || gameObject.name == "RSpawner3" || gameObject.name == "LSpawner3"|| gameObject.name == "UpSpawner2" || gameObject.name == "DownSpawner2"
            || gameObject.name == "UpSpawner3" || gameObject.name == "DownSpawner3" || gameObject.name == "UpSpawner4" || gameObject.name == "DownSpawner4")
        {
        }
        else
        {
            StartCoroutine(spawn());
        }

        gm = FindObjectOfType<GameManager>();
            
    }

    private void Update()
    {
        if (gameObject.name == "LRSpawner")
        {
            if (gm.wave >= 1)
            {
                if (gm.currentzombie == gm.zombiecount[gm.i] - 2)
                {
                    if (canBossSpawn)
                    {
                        Instantiate(BossMonster, transform.position, Quaternion.identity);
                    if (gm.currentzombie == gm.zombiecount[gm.i] - 1)
                    {
                        if (canBossSpawn)
                            {
                                Instantiate(BossMonster, transform.position, Quaternion.identity);
                                canBossSpawn = false;
                                samlldelay -= samlldelay * SpawnDelayUpValue;
                                bigdelay -= bigdelay * SpawnDelayUpValue;
                                StartCoroutine(delaySet());
                            }
                        }
                    } 
                } 
            }
            else
            {
                if (gm.currentzombie == gm.zombiecount[gm.i] - 1)
                {
                    if (canBossSpawn)
                    {
                        Instantiate(BossMonster, transform.position, Quaternion.identity);
                        canBossSpawn = false;
                        samlldelay -= samlldelay*SpawnDelayUpValue;
                        bigdelay -= bigdelay*SpawnDelayUpValue;
                        StartCoroutine(delaySet());
                    }
                } 
            }
        }
        else if (gameObject.name == "LSpawner" || gameObject.name == "RSpawner")
        {
            if (!btn)
            {
                if (gm.wave >= 5)
                {
                    StartCoroutine(spawn());
                    btn = true;
                }
            }
        }
        else if (gameObject.name == "LSpawner2" || gameObject.name == "RSpawner2")
        {
            if (!btn)
            {
                if (gm.wave >= 10)
                {
                    StartCoroutine(spawn());
                    btn = true;
                }
            }
        }
        else if (gameObject.name == "LSpawner3" || gameObject.name == "RSpawner3")
        {
            if (!btn)
            {
                if (gm.wave >= 15)
                {
                    StartCoroutine(spawn());
                    btn = true;
                }
            }
        }
        else if (gameObject.name == "UpSpawner2" || gameObject.name == "DownSpawner2")
        {
            if (!btn)
            {
                if (gm.wave >= 5)
                {
                    StartCoroutine(spawn());
                    btn = true;
                }
            }
        }
        else if (gameObject.name == "UpSpawner3" || gameObject.name == "DownSpawner3")
        {
            if (!btn)
            {
                if (gm.wave >= 10)
                {
                    StartCoroutine(spawn());
                    btn = true;
                }
            }
        }
        else if (gameObject.name == "UpSpawner4" || gameObject.name == "DownSpawner4")
        {
            if (!btn)
            {
                if (gm.wave >= 15)
                {
                    StartCoroutine(spawn());
                    btn = true;
                }
            }
        }
    }

    IEnumerator delaySet()
    {
        yield return new WaitForSeconds(5f);
        canBossSpawn = true; 
    }
    IEnumerator spawn()
    {
        yield return new WaitForSeconds(5f);
        gm.wave=1;
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(samlldelay, bigdelay));
            if (gm.currentzombie < gm.zombiecount[gm.i] && gm.currentzombie - 1 != gm.zombiecount[gm.i])
                {
                    if (gameObject.name == "UpSpawner")
                    {
                        GameObject mob = Instantiate(monster, transform.position, Quaternion.identity);
                        mob.GetComponent<SlimeScript>().StartCoroutine(mob.GetComponent<SlimeScript>().UpSpawner());
                    }
                    else if (gameObject.name == "DownSpawner")
                    {
                        GameObject mob = Instantiate(monster, transform.position, Quaternion.identity);
                        mob.GetComponent<SlimeScript>().StartCoroutine(mob.GetComponent<SlimeScript>().DownSpawner());
                    }
                    else
                    {
                        Instantiate(monster, transform.position, Quaternion.identity);
                    }
                }
        }
    }
}
