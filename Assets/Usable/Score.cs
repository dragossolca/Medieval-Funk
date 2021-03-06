using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Score : MonoBehaviour
{
    public int scoreValue = 0;
    TMP_Text scoreText;
    void Start()
    {
        scoreText = GetComponent<TMP_Text>();
    }

    
    void Update()
    {
        scoreText.text = "Score: " + scoreValue;
    }
}
