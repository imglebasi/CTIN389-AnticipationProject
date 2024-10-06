using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;

public class Clarity : MonoBehaviour
{
    public float clarity;
    //MOST clarity = 0 because its based on alpha
    public List<Image> Overlays = new List<Image>();
    public Color TargetColor;
    public Color testColor;

    //how long it takes for the filters to appear
    public float distortDuration;
    //time before the player recovers their clarity
    public float timeBeforeRecover;
    public bool recover;

    void Start()
    {
        clarity = 0;
        TargetColor.a = clarity;
        foreach (Image overlay in Overlays)
        {
            //change alpha to be 0 aka hidden
            overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
        }
    }

    void Update()
    {
        if(recover)
        {
            StartCoroutine(ClarityCooldown());
        }

    }
    public void UpdateClarity(float distortion)
    {
        if (distortion > 0)
        {
            recover = false;
        }

        //adding to clarity decreases it (IK ITS CONFUSING IDC) 
        clarity += distortion;
        TargetColor.a += distortion;

        foreach (Image overlay in Overlays)
        {
            overlay.color = TargetColor;
        }
    }
    public IEnumerator ClarityCooldown()
    {
        foreach(Image overlay in Overlays)
        {
            yield return overlay.DOFade(.1f, timeBeforeRecover);
        }
    }


    public IEnumerator ChangeColor(float newAlpha, Image overlay)
    {
        yield return overlay.DOFade(newAlpha, distortDuration);

        //after change color, start time before the player begins to regain clarity
        //StartCoroutine(ClarityCooldown());
    }
}
