﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public bool canSpawn = true;
    public float delayMinusValue=0;
    public float delayMinusDuration=0;
    public bool isFalling;
    public float minDelay,maxDelay;
    public GameObject[] onis;
    void Start()
    {
        if (isFalling)
            StartCoroutine(spawn2());
        else
            StartCoroutine(spawn());

        if (delayMinusDuration + delayMinusValue > 0)
            StartCoroutine(delayCor());

    }

    IEnumerator spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
            if (canSpawn)
            {
                GameObject oni = Instantiate(onis[Random.Range(0, onis.Length)], transform.position,
                    Quaternion.identity);
                if (oni.GetComponent<oniMove>().oniIndex == 2)
                    oni.transform.Translate(0, 1f, 0);

                oni.transform.Translate(0, UnityEngine.Random.Range(-0.2f, 0.2f), 0);
            }
        }
    }
    IEnumerator spawn2()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
            if (canSpawn)
            {
                GameObject oni = null;
                float r = Random.Range(GameObject.Find("Min").transform.position.x,
                    GameObject.Find("Max").transform.position.x);
                if (r <= -6.5f)
                    oni = Instantiate(onis[0], new Vector3(r - 2.25f, transform.position.y, 0),
                        Quaternion.identity); // 인덱스 0이 오른쪽으로 가는오니
                else if (r > -6.5f)
                    oni = Instantiate(onis[1], new Vector3(r + 2.4f, transform.position.y, 0),
                        Quaternion.identity); // 인덱스 1이 왼쪽으로 가는오니


                if (oni.GetComponent<oniMove>().oniIndex == 2)
                    oni.transform.Translate(0, 1f, 0);

                oni.transform.Translate(0, UnityEngine.Random.Range(-0.2f, 0.2f), 0);
            }
        }
    }
    IEnumerator delayCor()
    {
        while (true)
        {
            yield return new WaitForSeconds(delayMinusDuration);
            if (canSpawn)
            {
                minDelay -= delayMinusValue;
                maxDelay -= delayMinusValue;
            }
        }
    }
}