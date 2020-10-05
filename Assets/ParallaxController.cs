using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [SerializeField]
    float scrollSpeed = 1f;
    float repeatDistance = 32; //number of units on X axis before its time to move the background

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x >= -32f)
        {
            transform.position += Vector3.left * scrollSpeed * Time.deltaTime;

        }
        else
        {
            transform.position = Vector3.zero;
        }
    }
}
