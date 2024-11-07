using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;

public class Typer : MonoBehaviour
{
    public GlobalTyperVariables GlobalVariables;

    [Header("Text")]
    public WordBank SceneScript;

    public List<TextMeshProUGUI> Texts = new List<TextMeshProUGUI>() { };
    public TextMeshProUGUI backgroundText;
    public TextMeshProUGUI topText;

    public string remainingSentence = string.Empty;
    public string currentSentence = string.Empty;

    public string wrongColorModifier = "<color=#ff0000ff>";

    /*
    [Tooltip("where in the script the player is + who is talking")]
    [Header("Script Info")]
    public int bankIndex = 0;
    public bool npcSpeaking;
    */

    [Header("VFX")]
    public GameObject TextParent;
    public RectTransform Cursor;
    private Vector3 cursorOriginalPos;
    public float cursorInterval;
    public Vector3 shakeVector;
    public GameObject EndScreen;

    public Clarity ClarityManager;
    //how much clarity should go down per mistake
    public float clarityDecreasePerMistake;

    [Header("SFX")]
    public AudioManager theAudioManager;
    public float pitchVary;
    private Transform image;

    [Header("Affected Parties")]
    public List<NpcBehavior> NPCs = new List<NpcBehavior>() { };
    public Timer Timer;
    public TextMeshProUGUI BackgroundText;
    public int mistakeAmt = 0;

    private void Start()
    {
        theAudioManager.PlayPitch("Music", 1);

        EndScreen.SetActive(false);

        cursorOriginalPos = Cursor.position;

        //topText.text = "";

        GlobalVariables.ScriptIndex = 0;
        SetCurrentWord();
    }
    public void FixedUpdate()
    {

    }
    public void SetCurrentWord()
    {
        /*if (SceneScript.Dialog[GlobalVariables.ScriptIndex].EndsWith("0"))
        {

        }*/

        currentSentence = SceneScript.Dialog[GlobalVariables.ScriptIndex];

        /*if reach end of script
        if (SceneScript.GetComponent<WordBank>().Dialog[GlobalVariables.ScriptIndex] == null)
        {
            EndScreen.SetActive(true);
        }*/

        if (GlobalVariables.npcTalking == true && !GlobalVariables.isSentenceComplete)
        {
            //reset visual components
            Cursor.GetComponent<Image>().color = new Color(0, 0, 0, 255);
            Cursor.position = cursorOriginalPos;
            BackgroundText.text = currentSentence;
            topText.text = "";

            /*
            foreach(NpcBehavior script in NPCs) //so arms also get animated
            {
                script.idle = false;
                script.SetAnimation(GlobalVariables.npcTalking);
            }*/

            //have background text
            //SetBackgroundText(currentWord.TrimEnd('0'));
        }
        else if(GlobalVariables.npcTalking == false)
        {

            foreach (NpcBehavior script in NPCs) //so arms also get animated
            {
                script.SetAnimation(GlobalVariables.npcTalking);
            }

            //hide texts
            BackgroundText.text = "";
            topText.text = "";
        }

        SetRemainingSentence(currentSentence);
    }
    private void SetRemainingSentence(string newString)
    {
        remainingSentence = newString;
    }

    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        //EXCLUDE mouse clicks
        if (Input.anyKeyDown && !(Input.GetMouseButtonDown(0)
            || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)))
        {
            if (GlobalVariables.npcTalking)
            {
                theAudioManager.PlayPitch("Listening", pitchVary);
            }
            /*
            else
            {
                theAudioManager.PlayPitch("Speaking", pitchVary);
            }*/

            string keysPressed = Input.inputString;
            Debug.Log(keysPressed);

            //check if multiple keys pressed
            if (keysPressed.Length == 1)
            {
                EnterLetter(keysPressed);
            }
        }

    }

    private void EnterLetter(string typedLetter)
    {
        //regardless if right or wrong, move cursor to "correct" place
        if (typedLetter == " ")
        {
            //so have to space out a bit more
            StartCoroutine(MoveCursor(cursorInterval + 10));
        }
        else
        {
            StartCoroutine(MoveCursor(cursorInterval));
        }


        if (isCorrectLetter(typedLetter))
        {
            //Debug.Log("correct letter!");
            RemoveLetter();
            AddTopText("<color=#000000ff>" + typedLetter);

            if (isSentenceComplete())
            {
                GlobalVariables.isSentenceComplete = true;
                GlobalVariables.npcTalking = false;
                SetCurrentWord();

                //let timer know to set a new timer max
                //Timer.bankIndex = Timer.bankIndex + 1;
                //Timer.setTimerMax();
            }
        }

        //WRONG input while listening to npc talk
        else if (!isCorrectLetter(typedLetter))
        {
            //Debug.Log("incorrect letter.");

            //remove anyways
            RemoveLetter();

            AddTopText(wrongColorModifier + typedLetter);

            /*if (GlobalVariables.npcTalking) 
            {
                mistakeAmt += 1;
                //add letter BUT
                RemoveLetter();
                // decrease clarity
                ClarityManager.UpdateClarity(clarityDecreasePerMistake);
            }*/

            //no matter who talking: get wrong, get vfx and sfx
            //shake
            //StartCoroutine(Shake(TextParent));
            //play wrong input sound
            theAudioManager.PlayPitch("Wrong", 1);
            
            //sentence can be completed on an incorrect letter
            if (isSentenceComplete())
            {
                GlobalVariables.isSentenceComplete = true;
                GlobalVariables.npcTalking = false;
                SetCurrentWord();
            }
        }
    }

    private bool isCorrectLetter(string letter)
    {
        //if first letter, is correct letter
        return remainingSentence.IndexOf(letter) == 0;
    }
    private void RemoveLetter()
    {
        string newString = remainingSentence.Remove(0,1);
        SetRemainingSentence(newString);
    }

    public void AddTopText(string typedLetter)
    {
        topText.text = topText.text + typedLetter;
    }

    private bool isSentenceComplete()
    {
        //no more to type
        Cursor.position = cursorOriginalPos;
        Cursor.GetComponent<Image>().color = new Color(0,0,0,0);

        return remainingSentence.Length == 0;
    }



    public IEnumerator Shake(GameObject textobj)
    {
        yield return textobj.transform.DOShakePosition(0.2f, shakeVector, 10, 45, true, false, ShakeRandomnessMode.Full);
    }
    public IEnumerator MoveCursor(float amt)
    {
        yield return Cursor.DOLocalMoveX(Cursor.localPosition.x + amt, 0.1f, false);
    }

}
