using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private ScoreText scoreText;
    private int score;

    public GameObject spawner;
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
        AdjustDifficulty();
    }

    public int GetScore()
	{
        return score;
	}

    private void AdjustDifficulty()
	{
        if (GetScore() % 5 == 0)
            spawner.GetComponent<SpawnController>().IncreaseSpawnRate();
	}
}
