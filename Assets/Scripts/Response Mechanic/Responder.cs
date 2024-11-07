using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;

public class Responder : MonoBehaviour
{
    public GlobalTyperVariables GlobalVariables;

    public Canvas ResponseCanvas;

    //public bool isSentenceComplete = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(GlobalVariables.isSentenceComplete)
        {
            ResponseCanvas.enabled = true;
        }
        else
        {
            ResponseCanvas.enabled = false;
        }
    }

    public void Answer()
    {
        //trigger the next sentence in script
        GlobalVariables.ScriptIndex += 1;
        GlobalVariables.isSentenceComplete = false;
        GlobalVariables.npcTalking = true;
        GlobalVariables.Typer.GetComponent<Typer>().SetCurrentWord();
    }
}
