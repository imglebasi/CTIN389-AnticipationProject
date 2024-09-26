using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class WordBank : MonoBehaviour
{
    public List<string> sentences = new List<string>()
    {
    };

    //Dictionary<string> originalWordsandLengths 
    // ("so", 1), ("what", 2), ("brought", 3)
    //("so what brought you here today", 5), 

    //List<string> sentences
    //durationlimit for sentenceCompletion(letters in sentence * .1)

    //List<string> words
    //durationlimit for wordCompletion(letters in word * .1)
    //if mess up, decrease clarity 

    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
