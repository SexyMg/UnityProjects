﻿using System;
using UnityEngine;

public class crewMove : MonoBehaviour
{
    public bool isAngle = false;
    private Vector2 dir;
    private void Start()
    {
        if (isAngle)
        {
            transform.eulerAngles =
                new Vector3(0, 0, -getAngle(transform.position.x, transform.position.y, Player.instance.transform.position.x, Player.instance.transform.position.y)+180);
        }
        dir = Player.instance.transform.position - transform.position;
        dir.Normalize();
        Destroy(gameObject,5f);
    }

    void Update()
    {
        transform.position += (Vector3)dir * Time.deltaTime * 10;
       //transform.Translate(dir*Time.deltaTime*10);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Ground"))
            Destroy(gameObject);
    }
    private float getAngle(float x1, float y1, float x2, float y2) //Vector값을 넘겨받고 회전값을 넘겨줌
    {
        float dx = x2 - x1;
        float dy = y2 - y1;

        float rad = Mathf.Atan2(dx, dy);
        float degree = rad * Mathf.Rad2Deg;
        
        return degree;
    }
}