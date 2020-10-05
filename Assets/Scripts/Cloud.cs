using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 5)]
    float minSpeed = 1f;
    [SerializeField]
    [Range(1, 10)]
    float maxSpeed = 1;
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        if (maxSpeed < minSpeed) maxSpeed = minSpeed;

        speed = Random.Range(minSpeed, maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x <= -18) Destroy(this.gameObject); //if off screen destroy itself
    }
}
