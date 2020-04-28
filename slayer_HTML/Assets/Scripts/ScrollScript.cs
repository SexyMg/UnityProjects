﻿using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Image = UnityEngine.Experimental.UIElements.Image;

public class ScrollScript : MonoBehaviour, IBeginDragHandler,IDragHandler,IEndDragHandler
{
    private bool isNormal = true;
    public Button normal, hard;
 public Scrollbar scrollbar;
 public GameObject[] t;
 private Color Alpha125;
 private Color Alpha255;
    private const int SIZE = 2;
    private float[] pos = new float[SIZE];
    private float distance, curPos, targetPos;
    private bool isDrag;
    private int targetIndex;
    void Start()
    {
        Alpha125.r = 255;
        Alpha125.g = 255;
        Alpha125.b = 255;
        Alpha125.a = 0.5f;
        Alpha255.r = 255;
        Alpha255.g = 255;
        Alpha255.b = 255;
        Alpha255.a = 1f;
        FadePanel.instance.UnFade();
        //거리에 따라 0~1인 pos 대입
        distance = 1f / (SIZE - 1);
        for (int i = 0; i < SIZE; i++) pos[i] = distance * i;
        normal.GetComponent<UnityEngine.UI.Image>().color = Alpha255;
        hard.GetComponent<UnityEngine.UI.Image>().color = Alpha125;
        isNormal = true;
    }

    public void normalChange()
    {
        SoundManager.instance.select();
        normal.GetComponent<UnityEngine.UI.Image>().color = Alpha255;
        hard.GetComponent<UnityEngine.UI.Image>().color = Alpha125;
        isNormal = true;
    }
    public void hardChange()
    {
        SoundManager.instance.select();
        normal.GetComponent<UnityEngine.UI.Image>().color = Alpha125;
        hard.GetComponent<UnityEngine.UI.Image>().color = Alpha255;
        isNormal = false;
    }
    void Update()
    {
        if (!isDrag)
        {
            scrollbar.value = Mathf.Lerp(scrollbar.value, targetPos, 0.1f);
            for (int i = 0; i < SIZE; i++)
            {
                if(i==targetIndex)
                    t[i].SetActive(true);
                else
                    t[i].SetActive(false);
            }
        }
    }
    public void OnBeginDrag(PointerEventData eventData) => curPos = SetPos();
    
    public void OnDrag(PointerEventData eventData)=> isDrag = true;

    public void OnEndDrag(PointerEventData eventData)
    { 
        isDrag = false;

        targetPos = SetPos();
        
        //절반거리를 넘지 않아도 마우스를 빠르게 이동하려면
        if (curPos == targetPos) //처음드래그시작한 위치와 드래그 끝난 위치가 같다면
        {
            //스크롤이 왼쪽으로 빠르게 이동시 목표가 하나 감소
            if (eventData.delta.x > 18 && curPos - distance >= 0)
            {
                --targetIndex;
                targetPos = curPos - distance;
            }
            //스크롤이 오른쪽으로 빠르게 이동시 목표가 하나 증가
            else if (eventData.delta.x < -18 && curPos - distance <=1f)
            {
                if (targetIndex + 1 < SIZE)
                {
                    ++targetIndex;
                    targetPos = curPos + distance;
                }
            }
        }
    }

    public float SetPos()
    {
        //절반거리를 기준으로 가까운 위치를 반환
        for(int i=0;i<SIZE;i++)
            if (scrollbar.value < pos[i] + distance * 0.5f && scrollbar.value > pos[i] - distance * 0.5f)
            {
                targetIndex = i;
                return pos[i];
            }
        return 0;
    }
    public void SceneChange()
    {
        if (targetIndex == 0)
        {
            StartCoroutine(delayChange()); 
        }
        else if(targetIndex==1)
        {
            StartCoroutine(delayChange2());
        }
    }

    IEnumerator delayChange()
    {
        if (isNormal)
        {
            SoundManager.instance.tsuzumi(1);
            FadePanel.instance.Fade();
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("Main");
        }
        else
        {
            SoundManager.instance.tsuzumi(1);
            FadePanel.instance.Fade();
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("Main_H");
        }
    }
    IEnumerator delayChange2()
    {
        if (isNormal)
            {
                SoundManager.instance.Grass();
                FadePanel.instance.Fade();
                yield return new WaitForSeconds(1f);
                SceneManager.LoadScene("Main2");
            }
            else
            {
                SoundManager.instance.Grass();
                FadePanel.instance.Fade();
                yield return new WaitForSeconds(1f);
                SceneManager.LoadScene("Main2_H");
            }
    }
}