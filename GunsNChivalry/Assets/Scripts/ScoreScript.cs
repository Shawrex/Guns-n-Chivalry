using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public int score = 0;
    [SerializeField] private Text scoreText = null;

    public static ScoreScript instance;

    void Start()
    {
        scoreText.text = "" + score;

        if (instance == null)
            instance = this;
    }

    public void ChangeScore(int amount)
    { 
        score += amount;
        scoreText.text = "" + score;
    }
}
