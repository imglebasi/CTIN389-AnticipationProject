using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;

public class Typer : MonoBehaviour
{
    public WordBank wordBank = null;
    public TextMeshProUGUI wordOutput = null;
    public TextMeshProUGUI typedOutput = null;
    public GameObject typedWords;

    private string remainingWord = string.Empty;
    private string currentWord = string.Empty;
    //private string currentWord = string.Empty;
    public int bankIndex = 0;

    public Vector3 shakeVector; 

    private void Start()
    {
        typedOutput.text = "";
        SetCurrentWord();
    }
    private void SetCurrentWord()
    {
        currentWord = wordBank.GetComponent<WordBank>().sentences[bankIndex];
        //currentWord = wordBank.GetWord();
        //currentWord = currentWord;
        SetRemainingWord(currentWord);
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

            //type what player is typing
            typedOutput.text = typedOutput.text + typedLetter;

            //reminder to change to isSENTENCEComplete, not word
            if (isWordComplete())
            {
                bankIndex = bankIndex + 1;
                SetCurrentWord();
            }
        }
        //WRONG input
        else if (!isCorrectLetter(typedLetter))
        {
            StartCoroutine(ShakeText(typedWords));
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
    private bool isWordComplete()
    {
        //no more to type
        return remainingWord.Length == 0;
    }

    public IEnumerator ShakeText(GameObject textobj)
    {
        yield return textobj.transform.DOShakePosition(0.2f, shakeVector, 10, 45, true, false, ShakeRandomnessMode.Full);
    }
}
