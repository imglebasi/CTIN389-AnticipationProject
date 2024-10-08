using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;

public class Typer : MonoBehaviour
{
    [Header("Text")]
    public WordBank wordBank = null;
    public TextMeshProUGUI wordOutput;
    public List<TextMeshProUGUI> Texts = new List<TextMeshProUGUI>() { };
    private string remainingWord = string.Empty;
    private string currentWord = string.Empty;

    [Tooltip("where in the script the player is + who is talking")]
    [Header("Script Info")]
    public int bankIndex = 0;
    public bool npcSpeaking;

    [Header("VFX")]
    public GameObject TextParent;
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

        SetCurrentWord();
    }
    public void FixedUpdate()
    {

    }
    public void SetCurrentWord()
    {
        //string should end with a character id (0 for creep, 1 for player)
        //can set which characters turn it is to talk
        currentWord = wordBank.GetComponent<WordBank>().Dialog[bankIndex];
        
        //if reach end of script
        if(wordBank.GetComponent<WordBank>().Dialog[bankIndex] == null)
        {
            //end of script
            EndScreen.SetActive(true);
        }

        //this is so i can use lists essentially as a dictionary bc dict cant be public
        if (currentWord.EndsWith("0"))
        {
            npcSpeaking = true;

            foreach(NpcBehavior script in NPCs) //so arms also get animated
            {
                script.idle = false;
                script.SetAnimation(npcSpeaking);
            }

            wordOutput = Texts[0];
            //Debug.Log(wordOutput);
            Texts[1].text = "";
            //Debug.Log("NPC talking");

            //have background text
            //SetBackgroundText(currentWord.TrimEnd('0'));
        }
        else if(currentWord.EndsWith("1"))
        {
            npcSpeaking = false;

            foreach (NpcBehavior script in NPCs) //so arms also get animated
            {
                script.idle = true;
                script.SetAnimation(npcSpeaking);
            }

            wordOutput = Texts[1];
            Texts[0].text = "";
            Debug.Log("Player talking");

            //hide background text
            //BackgroundText.text = "";
        }

        //Debug.Log("about to trim because of SetRemainingWord");
        SetRemainingWord(currentWord.TrimEnd('0','1'));
    }
    private void SetRemainingWord(string newString)
    {
        remainingWord = newString;
        wordOutput.text = remainingWord;
    }

    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (Input.anyKeyDown)
        {
            if (npcSpeaking)
            {
                theAudioManager.PlayPitch("Listening", pitchVary);
            }
            else
            {
                theAudioManager.PlayPitch("Speaking", pitchVary);
            }
            string keysPressed = Input.inputString;

            //check if multiple keys pressed
            if (keysPressed.Length == 1)
            {
                EnterLetter(keysPressed);
            }
        }

    }

    private void EnterLetter(string typedLetter)
    {
        if (isCorrectLetter(typedLetter))
        {
            //Debug.Log("correct letter!");
            RemoveLetter();

            //reminder to change to isSENTENCEComplete, not word
            if (isSentenceComplete())
            {
                bankIndex = bankIndex + 1;
                SetCurrentWord();

                //let timer know to set a new timer max
                Timer.bankIndex = Timer.bankIndex + 1;
                Timer.setTimerMax();
            }
        }
        //WRONG input while listening to npc talk
        else if (!isCorrectLetter(typedLetter))
        {
            //Debug.Log("incorrect letter.");
            if (npcSpeaking) 
            {
                mistakeAmt += 1;
                //remove letter BUT
                RemoveLetter();
                // decrease clarity
                ClarityManager.UpdateClarity(clarityDecreasePerMistake);
            }

            //no matter who talking: get wrong, get vfx and sfx
            //shake
            StartCoroutine(Shake(TextParent));
            //play wrong input sound
            theAudioManager.PlayPitch("Wrong", 1);
            
            //sentence can be completed on an incorrect letter
            if (isSentenceComplete())
            {
                bankIndex =+ 1;
                SetCurrentWord();
            }
        }
    }
    private bool isCorrectLetter(string letter)
    {
        //if first letter, is correct letter
        return remainingWord.IndexOf(letter) == 0;
    }
    private void RemoveLetter()
    {
        string newString = remainingWord.Remove(0, 1);
        SetRemainingWord(newString);
    }
    private bool isSentenceComplete()
    {
        //no more to type
        return remainingWord.Length == 0;
    }

    public void SetBackgroundText(string currentSentence)
    {
        BackgroundText.text = currentSentence;
    }
    public IEnumerator Shake(GameObject textobj)
    {
        yield return textobj.transform.DOShakePosition(0.2f, shakeVector, 10, 45, true, false, ShakeRandomnessMode.Full);
    }

}
