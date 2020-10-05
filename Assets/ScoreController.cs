using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public Text scoreCard;
    private int score;
    void Start()
    {
        ResetScore();
    }

    public void ResetScore()
	{
        score = 0;
        UpdateScore();
    }

    public void StarHit()
	{
        score += 1;
        UpdateScore();
    }

    public int GetScore()
	{
        return score;
	}
    private void UpdateScore()
	{
        scoreCard.text = "Score: " + GetScore();
	}
}
