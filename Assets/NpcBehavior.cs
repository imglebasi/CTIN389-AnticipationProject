using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;

public class NpcBehavior : MonoBehaviour
{
    [Header("Conditions")]
    public bool npcTalking;
    public bool idle;

    [Header("Animations Info")]
    [Tooltip("which animation the animator is playing")]
    //0 is idle, 1 is talking, 2-?? is mannerisms
    public int mannerismAmt = 3;
    public string theAnimation;
    //dictates delay between mannerism anim to idle anim
    public List<float> mannerismLength = new List<float>() { 1f, 2f, 3f };

    private Animator _Animator;
    private int whichAnim;

    [Tooltip("adjust feel of NPC animation frequency")]
    [Header("Values")]
    //the delay between idle and mannerism animations
    [Range(0.1f, 5.5f)]
    public float idleToAnimMin;
    [Range(0.1f, 5.5f)]
    public float idleToAnimMax;
    //returned value, dont think it has to be public tbh
    private float itaDuration;

    //idk if we'll need this- how does npc transition back to idle? after completion?
    /*[Range(0.1f, 5.5f)]
    public float animToIdleMin;
    [Range(0.1f, 5.5f)]
    public float animToIdleMax;
    */

    void Start()
    {
       _Animator = GetComponent<Animator>();
    }

    void Update()
    {
        //npc is talking
        if (npcTalking)
        {
            theAnimation = "talking";
            _Animator.SetInteger(0, 0);
        }
        //player is talking and npc is idle
        else if (!npcTalking && idle)
        {
            //set animation to idle
            theAnimation = "idle";
            _Animator.SetInteger(0, 1);

            //start a countdown to the next mannerism anim
            itaDuration = Random.Range(idleToAnimMin,idleToAnimMax);
            StartCoroutine(idleToAnimDelay(itaDuration));
        }
        //player is talking and npc is NOT idle, doing a mannerism
        else if(!npcTalking && !idle)
        {
            Debug.Log("doing a mannerism");
        }
    }

    public IEnumerator idleToAnimDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        //not idle, going to perform a mannerism
        idle = false;

        //set controller to a random mannerism

        //CANT BE 0 or 1 (talking and idle anim (has to be 2-??)
        whichAnim = Random.Range(2, mannerismAmt+1);
        theAnimation = "a mannerism";
        _Animator.SetInteger(0, whichAnim);
        
        //start countdown to switch BACK to idle
        StartCoroutine(animToIdleDelay(whichAnim));
    }

    //depending on which mannerism is playing, delay between anim and idle diff
    public IEnumerator animToIdleDelay(int whichAnim)
    {
        //wait diff lengths (set in list) depending on the mannerism
        if(whichAnim == 2)
        {
            yield return new WaitForSeconds(mannerismLength[0]);
            //now switch back to idle
            idle = true;
        }
        else if (whichAnim == 3)
        {
            yield return new WaitForSeconds(mannerismLength[1]);
            idle = true;
        }
        else if(whichAnim == 4)
        {
            yield return new WaitForSeconds(mannerismLength[2]);
            idle = true;
        }
    }
}
