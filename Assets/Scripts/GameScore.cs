using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class GameScore : MonoBehaviour
{
    TMP_Text scoreTextUI;

    int score;

    public int Score
    {
        get
        {
            return this.score;
        }
        set
        {
            this.score = value;
            UpdateScoreTextUI();
        }
    }
    void Awake()
    {
        scoreTextUI = GetComponent<TMP_Text>();
        if (scoreTextUI == null)
        {
            Debug.LogError("O componente Text não foi encontrado no GameObject!");
        }
    }

    //Function to update the score text UI
    void UpdateScoreTextUI()
    {
        string scoreStr = string.Format("{0:0000000}", score);
        scoreTextUI.text = scoreStr;
    }
}
