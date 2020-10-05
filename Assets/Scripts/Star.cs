using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public ScoreController score;

    void Start()
	{
        score = FindObjectOfType<ScoreController>();
	}

    public void Update()
    {
        UpdatePosition(Time.deltaTime);
    }

    void UpdatePosition(float delta)
    {
        this.transform.localPosition += (Vector3.left * delta);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
		{
            score.StarHit();
            Destroy(this.gameObject);
        }
    }
}
