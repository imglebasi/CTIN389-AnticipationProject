using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;

public class Typer : MonoBehaviour
{
    public WordBank wordBank = null;
    public TextMeshProUGUI wordOutput;
    public List<TextMeshProUGUI> Texts = new List<TextMeshProUGUI>() { };

    private string remainingWord = string.Empty;
    private string currentWord = string.Empty;
    public int bankIndex = 0;

    public GameObject TextParent;
    public Vector3 shakeVector; 

    private void Start()
    {
        //typedOutput.text = "";
        SetCurrentWord();
    }
    private void SetCurrentWord()
    {
        //string should end with a character id (0 for creep, 1 for player)
        //can set which characters turn it is to talk
        currentWord = wordBank.GetComponent<WordBank>().Dialog[bankIndex];

        //this is so i can use lists essentially as a dictionary bc dict cant be public
        if (currentWord.EndsWith("0"))
        {
            wordOutput = Texts[0];
            Texts[1].text = "";
            Debug.Log("NPC talking");
        }
        else if (currentWord.EndsWith("1"))
        {
            wordOutput = Texts[1];
            Texts[0].text = "";
            Debug.Log("Player talking");
        }

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
            RemoveLetter();

            //reminder to change to isSENTENCEComplete, not word
            if (isSentenceComplete())
            {
                bankIndex = bankIndex + 1;
                SetCurrentWord();
            }
        }
        //WRONG input
        else if (!isCorrectLetter(typedLetter))
        {
            StartCoroutine(Shake(TextParent));
            Debug.Log("wrong letter!");
        }
    }
    private bool isCorrectLetter(string letter)
    {
        //if first letter is correct letter
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

    public IEnumerator Shake(GameObject textobj)
    {
        yield return textobj.transform.DOShakePosition(0.2f, shakeVector, 10, 45, true, false, ShakeRandomnessMode.Full);
    }
}
