﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScorePanel : MonoBehaviour
{
    public bool isTrain = false;
    public GameObject bestScore;
    private bool isBest = false;
    public Text bonusText1,bonusText2;
    public float textDelay;
    public int headScore,maxComboScore;
    public Text head, killedOni, maxCombo, score;
    private int headv, killedOniv, scorev, maxcombov;
    private int headc = 0, killedOnic = 0, scorec=0,maxcomboc = 0;
    
    private string highScoreKey1 = "highScoreKey1";
    private string highScoreKey2 = "highScoreKey2";
    private string fastTimeKeyN = "fastTimeKeyN";
    private int highScore1;
    private int highScore2;
    private int Traintime;
    private string highScoreKey1_H = "highScoreKey1_H";
    private string highScoreKey2_H = "highScoreKey2_H";
    private string fastTimeKeyH = "fastTimeKeyH";
    private int highScore1_H;
    private int highScore2_H;
    private int Traintime_H;
    private string highComboKey1 = "highComboKey1";
    private string highComboKey2 = "highComboKey2";
    private int highCombo1;
    private int highCombo2;

    public IEnumerator bonus()
    {
        if (SceneManager.GetActiveScene().name == "Main3_EZ"||SceneManager.GetActiveScene().name == "Main3"||SceneManager.GetActiveScene().name == "Main3_H")
        {
            if(Player.instance.playerIndex==1) 
                GameObject.Find("kaguraObj").gameObject.SetActive(false);
        }
        //점수 불러오기
        highScore1 = PlayerPrefs.GetInt(highScoreKey1, 0);
        highScore2 = PlayerPrefs.GetInt(highScoreKey2, 0);
        highScore1_H = PlayerPrefs.GetInt(highScoreKey1_H, 0);
        highScore2_H = PlayerPrefs.GetInt(highScoreKey2_H, 0);
        highCombo1 = PlayerPrefs.GetInt(highComboKey1, 0);
        highCombo2 = PlayerPrefs.GetInt(highComboKey2, 0);
        Traintime = PlayerPrefs.GetInt(fastTimeKeyN, 999);
        Traintime_H=PlayerPrefs.GetInt(fastTimeKeyH, 999);

        if (SceneManager.GetActiveScene().name == "Main") //스테이지 1
        {
            //점수
            if (ScoreMgr.instance.score + (headv * headScore) + (maxcombov * maxComboScore) > highScore1)
            {
                isBest = true;
                highScore1 = ScoreMgr.instance.score + (headv * headScore) + (maxcombov * maxComboScore);
                PlayerPrefs.SetInt(highScoreKey1,ScoreMgr.instance.score + (headv * headScore) + (maxcombov * maxComboScore));
            }
            //콤보
            if (maxcombov > highCombo1)
            {
                highCombo1 = maxcombov;
                PlayerPrefs.SetInt(highComboKey1,maxcombov);
            }
        }
        else if (SceneManager.GetActiveScene().name == "Main2") //스테이지 2
        {
            //점수
            if (ScoreMgr.instance.score + (headv * headScore) + (maxcombov * maxComboScore) > highScore2)
            {
                isBest = true;
                highScore2 = ScoreMgr.instance.score + (headv * headScore) + (maxcombov * maxComboScore);
                PlayerPrefs.SetInt(highScoreKey2,ScoreMgr.instance.score + (headv * headScore) + (maxcombov * maxComboScore));
            }
            //콤보
            if (maxcombov > highCombo2)
            {
                highCombo2 = maxcombov;
                PlayerPrefs.SetInt(highComboKey2,maxcombov);
            }
        }
        else if (SceneManager.GetActiveScene().name == "Main_H") //스테이지 1 하드
        {
            //점수
            if (ScoreMgr.instance.score + (headv * headScore) + (maxcombov * maxComboScore) > highScore1_H)
            {
                isBest = true;
                highScore1_H = ScoreMgr.instance.score + (headv * headScore) + (maxcombov * maxComboScore);
                PlayerPrefs.SetInt(highScoreKey1_H,ScoreMgr.instance.score + (headv * headScore) + (maxcombov * maxComboScore));
            }
        }
        else if (SceneManager.GetActiveScene().name == "Main2_H") //스테이지 2 하드
        {
            //점수
            if (ScoreMgr.instance.score + (headv * headScore) + (maxcombov * maxComboScore) > highScore2_H)
            {
                isBest = true;
                highScore2_H = ScoreMgr.instance.score + (headv * headScore) + (maxcombov * maxComboScore);
                PlayerPrefs.SetInt(highScoreKey2_H,ScoreMgr.instance.score + (headv * headScore) + (maxcombov * maxComboScore));
            }
        }
        else if (SceneManager.GetActiveScene().name == "Main3") //스테이지 3a
        {
            if (Player.instance != null) //플레이어가 죽지 않았다면
            {
                print("시간 : "+GameManager.instance.trainTime+" 기존기록 : "+Traintime);
                //점수
                if (GameManager.instance.trainTime < Traintime)
                {
                    isBest = true;
                    Traintime = GameManager.instance.trainTime;
                    PlayerPrefs.SetInt(fastTimeKeyN, Traintime);
                }
            }
        }
        else if (SceneManager.GetActiveScene().name == "Main3_H") //스테이지 3
        {
            if (Player.instance != null)
            {
                //점수
                if (GameManager.instance.trainTime < Traintime_H)
                {
                    isBest = true;
                    Traintime_H = GameManager.instance.trainTime;
                    PlayerPrefs.SetInt(fastTimeKeyH, Traintime_H);
                }
            }
        }
        
        yield return new WaitForSecondsRealtime(0.3f);
 if (isTrain)
 {
     GameObject.Find("trainText").GetComponent<Text>().text =
         "시간 : " + GameManager.instance.trainTime / 60 +"분 "+ GameManager.instance.trainTime % 60+"초";
 }
        else
        {
            head.text = "급소 공격 횟수 : 0번";
            killedOni.text = "죽인 오니 : 0마리";
            maxCombo.text = "최대 콤보 횟수 : 0번";
            score.text = ScoreMgr.instance.score.ToString();
            scorec = ScoreMgr.instance.score;
            headv = ScoreMgr.instance.headshot;
            killedOniv=ScoreMgr.instance.killedOni;
            scorev = ScoreMgr.instance.score;
            maxcombov = ScoreMgr.instance.maxCombo;   
                    while (killedOnic<killedOniv)
        {
            yield return new WaitForSecondsRealtime(textDelay);
            if (killedOnic + 1 == killedOniv)
                killedOnic++;
            else
                killedOnic+=2;
            
            if (killedOnic% 3 == 0)
                SoundManager.instance.scoreCount();
                killedOni.text = "죽인 오니 : " + killedOnic + "마리";
        }
        yield return new WaitForSecondsRealtime(0.3f);
        if (headv > 0)
        {
            while (headc < headv)
            {
                yield return new WaitForSecondsRealtime(textDelay);
                if (headc + 1 == headv)
                    headc++;
                else
                    headc+=2;
                if (headc% 3 == 0)
                    SoundManager.instance.scoreCount();
                head.text = "급소 공격 횟수 : " + headc + "번";
            }

            yield return new WaitForSecondsRealtime(0.3f);
        }
        if (maxcombov > 1)
        {
            while (maxcomboc < maxcombov)
            {
                yield return new WaitForSecondsRealtime(textDelay);
                maxcomboc++;
                if (maxcomboc % 3 == 0)
                    SoundManager.instance.scoreCount();
                maxCombo.text = "최대 콤보 횟수 : " + maxcomboc + "번";
            }

            yield return new WaitForSecondsRealtime(0.3f);
        }

        if (headv > 0)
        {
            bonusText1.text = "급소 보너스 + " + headv * headScore;
            while (scorec < ScoreMgr.instance.score + (headv * headScore))
            {
                yield return new WaitForSecondsRealtime(textDelay);
                scorec += 25;
                if (scorec % 75 == 0)
                    SoundManager.instance.scoreCount();
                score.text = scorec.ToString();
            }

            yield return new WaitForSecondsRealtime(0.2f);
        }

        if (maxcombov > 1)
        {
            bonusText2.text = "최대콤보 보너스 + " + maxcombov * maxComboScore;
            while (scorec < ScoreMgr.instance.score + (headv * headScore) + (maxcombov * maxComboScore))
            {
                yield return new WaitForSecondsRealtime(textDelay);
                scorec += 25;
                if (scorec % 75 == 0)
                    SoundManager.instance.scoreCount();
                score.text = scorec.ToString();
            }

            yield return new WaitForSecondsRealtime(0.2f);
        }

        }
        if (isBest)
        {
            SoundManager.instance.bestScore();
            bestScore.SetActive(true);
        }
    }
}
