using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotBalloon : MonoBehaviour
{

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
        GameObject plane = collision.gameObject;

        if (plane.GetComponent<BezierFollow>())
        {
            plane.GetComponent<BezierFollow>().enabled = false;
            plane.GetComponent<OrbitController>().SetDead();
            plane.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
