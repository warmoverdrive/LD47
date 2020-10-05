﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotBalloon : MonoBehaviour
{
    public GameObject plane;

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
        Debug.Log("Hot Balloon Hit!");
        plane.GetComponent<BezierFollow>().enabled = false;
        plane.GetComponent<OrbitController>().SetDead();
        plane.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
}
