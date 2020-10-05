using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField]
    private float minTimeToSpawn = 3, maxTimeToSpawn = 8, minY = -7, maxY = 7;
    public GameObject[] entities;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 2, Random.Range(minTimeToSpawn, maxTimeToSpawn));
    }

    private void Spawn()
	{
        Vector3 spawnposition = this.transform.position;
        spawnposition.y = Random.Range(minY, maxY);
        Instantiate(entities[Random.Range(0, entities.Length - 1)], spawnposition, Quaternion.identity, this.transform);
    }

    public void IncreaseSpawnRate()
	{
        minTimeToSpawn--;
        maxTimeToSpawn--;
        if (minTimeToSpawn < 1) minTimeToSpawn = 1;
        if (maxTimeToSpawn < 2) maxTimeToSpawn = 2;
        CancelInvoke();
        InvokeRepeating("Spawn", 2, Random.Range(minTimeToSpawn, maxTimeToSpawn));
    }

    public void ResetSpawnRate()
	{
        minTimeToSpawn = 3;
        maxTimeToSpawn = 8;
        CancelInvoke();
        InvokeRepeating("Spawn", 2, Random.Range(minTimeToSpawn, maxTimeToSpawn));
    }
}
