using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public ScoreController score;

    void Start()
	{
        score = (ScoreController)GameObject.Find("Plane").GetComponent<ScoreController>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        score.StarHit();
        Destroy(this.gameObject);
    }
}
