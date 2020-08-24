using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public int money = 0;
    [SerializeField] private Text scoreText = null;

    public static ScoreScript instance;

    void Start()
    {
        scoreText.text = "" + money;

        if (instance == null)
            instance = this;
    }

    public void ChangeScore(string type, int amount)
    { 
        if (type == "money")
        {
            money += amount;
            scoreText.text = "" + money;
        }
    }
}
