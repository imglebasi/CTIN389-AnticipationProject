using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;

public class NpcBehavior : MonoBehaviour
{
    [Header("Conditions")]
    public GameObject theTyper;
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
    }

    public void SetAnimation(bool talking)
    {

        npcTalking = theTyper.GetComponent<Typer>().npcSpeaking;

        //npc is talking
        if (talking)
        {
            idle = false;
            theAnimation = "talking";
            _Animator.SetInteger("animation", 1);
        }
        //player is talking and npc is idle
        else if ((!talking) && idle)
        {
            Debug.Log("npc is idle");
            //set animation to idle
            theAnimation = "idle";
            _Animator.SetInteger("animation", 0);

            //start a countdown to the next mannerism anim
            itaDuration = Random.Range(idleToAnimMin, idleToAnimMax);
            Debug.Log("itaDuration: " + itaDuration);
            StartCoroutine(idleToAnimDelay(itaDuration));
        }
        //player is talking and npc is NOT idle, doing a mannerism
        else if ((!talking) && (!idle))
        {
            Debug.Log("doing a mannerism");

            //set controller to a random mannerism

            //CANT BE 0 or 1 (talking and idle anim (has to be 2-??)
            whichAnim = Random.Range(2, 5);
            theAnimation = "a mannerism: " + whichAnim;
            _Animator.SetInteger("animation", whichAnim);

            //start countdown to switch BACK to idle
            //StartCoroutine(animToIdleDelay());
        }
    }

    public IEnumerator idleToAnimDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        //not idle, going to perform a mannerism
        idle = false;
        SetAnimation(false);
    }


    //dont think i need/cant figure out
    //depending on which mannerism is playing, delay between anim and idle diff
    public IEnumerator animToIdleDelay()
    {
        yield return new WaitForSeconds(2);
        idle = true;
        SetAnimation(false);

        /*
        //too lazy to make this work- just wait a set time of 2

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
        }*/
    }
}
