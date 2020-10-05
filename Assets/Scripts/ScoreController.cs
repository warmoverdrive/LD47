using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private ScoreText scoreText;
    private int score;
    void Start()
    {
        scoreText = FindObjectOfType<ScoreText>();
        ResetScore();
    }

    public void ResetScore()
	{
        score = 0;
        scoreText.UpdateText(score.ToString());
	}

    public void StarHit()
	{
        score += 1;
        scoreText.UpdateText(score.ToString());
    }

    public int GetScore()
	{
        return score;
	}
}
