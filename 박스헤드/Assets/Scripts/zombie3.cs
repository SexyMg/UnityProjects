﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombie3 : MonoBehaviour
{
    private GameObject obj;
    private Transform target; // 따라갈 물체의 방향
    public float speed = 1.0f;

    private void Start()
    {
        obj = this.gameObject;
    }

    void Update()
    {
        Move1 player = GameObject.Find("player").GetComponent<Move1>();
        target = player.transform;
        Vector3 dir = target.position - transform.position; //사이의 거리를 구함
        transform.position +=
            new Vector3(Mathf.Clamp(dir.x, speed * -1, speed), Mathf.Clamp(dir.y, speed * -1, speed), dir.z) * speed *
            Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("banana"))
        {
            Debug.Log("좀비에 바나나 닿음!");
            GameOver gameover = GameObject.Find("Canvas").GetComponent<GameOver>();
            gameover.zombiecount += 1;
            Destroy(obj);
        }
    }
}
