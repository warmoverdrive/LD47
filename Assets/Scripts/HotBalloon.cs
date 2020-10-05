using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotBalloon : MonoBehaviour
{

    float lateralSpeed;
    float randomJitter;

	public void Update()
	{
        UpdatePosition(Time.deltaTime);
        lateralSpeed = Random.Range(-5.0f, -1.0f);
        randomJitter = Random.Range(0f, 1.0f);
    }

    void UpdatePosition(float delta)
	{
        this.transform.localPosition += (new Vector3(lateralSpeed, Mathf.Sin(Time.time + randomJitter), 0) * delta);
	}

	private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject plane = collision.gameObject;

        if (plane.GetComponent<BezierFollow>())
        {
            plane.GetComponent<BezierFollow>().enabled = false;
            plane.GetComponent<OrbitController>().SetDead();
            plane.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
