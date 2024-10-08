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
    public Image Outline;

    [Header("Timer Values")]
    public float timeRemaining;
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
    public Clarity ClarityManager;
    [Tooltip("if timer runs out, how much clarity decreases")]
    public float clarityDecrease;

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
                timeRemaining = timeMax;
                timerReset = true;
            }
            Fill.fillAmount = timeRemaining / timeMax;
            //Debug.Log(Fill.fillAmount);
            timeRemaining -= Time.deltaTime;

            TimerText.text = "" + timeRemaining;
            //alt
            //TimerText.text = "" + (int)time;
            
            if (timeRemaining < 0)
            {
                //Debug.Log("time ran out");
                timeRemaining = 0;
                //increase distortion
                ClarityManager.UpdateClarity(clarityDecrease);

                //move onto next sentence
                Typer.GetComponent<Typer>().bankIndex += 1;
                Typer.GetComponent<Typer>().SetCurrentWord();
                bankIndex += 1;

                //stop timer
                runTimer = false;
                //run timerMax
                setTimerMax();
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
            //reset cause we abt to calculate a new timeMax to use
            timeMax = 0;

            //show timer
            Fill.color = new Color(255, 255, 255, 200);
            Outline.color = new Color(255, 255, 255, 200);

            //get rid of extrenous 0
            currentSentence.TrimEnd('0');

            foreach (char character in currentSentence)
            {
                timeMax += perCharacterTime;
            }
            if (currentSentence.Contains("."))
            {
                timeMax += perPeriodTime;
            }

            //Debug.Log(timeMax);

            //reset time
            timeRemaining = timeMax;

            //start timer
            runTimer = true;

        }
        else if (currentSentence.EndsWith("1")) //player is talking so...
        {
            //hide timer
            Fill.color = new Color(255, 255, 255, 0);
            Outline.color = new Color(255, 255, 255, 0);

            //dont let it run
            runTimer = false;
            Debug.Log("timer off, is players turn");
        }
    }
}
