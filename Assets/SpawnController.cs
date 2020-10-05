using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject[] entities;

    private int tMin = 3;
    private int tMax = 8;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 2, Random.Range(tMin, tMax));
    }

    private void Spawn()
	{
        Vector3 spawnposition = this.transform.position;
        spawnposition.y = Random.Range(-7, 5);
        Instantiate(entities[Random.Range(0, entities.Length - 1)], spawnposition, Quaternion.identity);
    }
}
