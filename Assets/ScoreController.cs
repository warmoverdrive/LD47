using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private int score;
    void Start()
    {
        ResetScore();
    }

    public void ResetScore()
	{
        score = 0;
	}

    public void StarHit()
	{
        score += 1;
	}

    public int GetScore()
	{
        return score;
	}
}
