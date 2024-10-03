using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;

public class Timer : MonoBehaviour
{
    [Header("Timer")]
    public TextMeshProUGUI TimerText;
    public Image Fill;
    //public Image timerCirlce;

    [Header("Timer Values")]
    public float time;
    public float timeMax;
    //how much time is given per character
    public float perCharacterTime;
    public float perPeriodTime;
    public bool runTimer;
    public bool timerReset;

    [Header("Needed Info")]
    public GameObject Typer;
    public WordBank Dialog;
    public int bankIndex = 0;
    public string currentSentence;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        setTimerMax();
    }

    // Update is called once per frame
    void Update()
    {
        if (runTimer)
        {
            if (!timerReset)
            {
                time = timeMax;
                timerReset = true;
            }
            Fill.fillAmount = time / timeMax;
            //Debug.Log(Fill.fillAmount);
            time -= Time.deltaTime;

            TimerText.text = "" + time;
            //alt
            //TimerText.text = "" + (int)time;
            
            if (time < 0)
            {
                //Debug.Log("time ran out");
                time = 0;
                //uhhh gg idk 
            }
        }
    }

    public void setTimerMax()
    {
        //reset timer here?? idk
        //timerReset = false;

        currentSentence = Dialog.Dialog[bankIndex];

        if (currentSentence.EndsWith("0"))
        {
            //get rid of extrenous 0
            currentSentence.TrimEnd('0');

            //turn on timer
            //timerCirlce.enabled = true;

            foreach (char character in currentSentence)
            {
                timeMax += perCharacterTime;
            }
            if (currentSentence.Contains("."))
            {
                timeMax += perPeriodTime;
            }

            //Debug.Log(timeMax);
            //start timer
            runTimer = true;

        }
        else if (currentSentence.EndsWith("1")) //player is talking so...
        {
            //hide timer
            //timerCirlce.enabled = false;
            //dont let it run
            runTimer = false;
            Debug.Log("timer off, is players turn");
        }
    }
}
