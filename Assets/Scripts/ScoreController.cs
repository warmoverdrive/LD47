using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private ScoreText[] scoreTexts;
    private int score;
    void Start()
    {
        scoreTexts = FindObjectsOfType<ScoreText>();
        ResetScore();
    }

    public void ResetScore()
	{
        foreach (ScoreText scoreText in scoreTexts)
        {
            score = 0;
            scoreText.UpdateText(score.ToString());
        }
	}

    public void StarHit()
	{
        foreach (ScoreText scoreText in scoreTexts)
        {
            score += 1;
            scoreText.UpdateText(score.ToString());
        }
    }

    public void SendScore()
    {
        foreach (ScoreText scoreText in scoreTexts)
        {
            scoreText.UpdateText(score.ToString());
        }
    }

    public int GetScore()
	{
        return score;
	}
}
