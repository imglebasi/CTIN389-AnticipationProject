using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;

public class InfoDisplay : MonoBehaviour
{
    public TextMeshProUGUI Distortion;
    public Clarity ClarityManager;

    public TextMeshProUGUI Mistakes;
    public Typer TyperManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Distortion.text = "distortion: " + ClarityManager.clarity;
        Mistakes.text = "mistakes: " + TyperManager.mistakeAmt;
    }
}
