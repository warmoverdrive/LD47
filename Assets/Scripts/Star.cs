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
        score.StarHit();
        Destroy(this.gameObject);
    }
}
