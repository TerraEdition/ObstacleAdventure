using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerator : MonoBehaviour
{
    public GameObject[] clouds;
    private float maxSpawnRateInSeconds = 3f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnClouds", maxSpawnRateInSeconds);
    }
    void SpawnClouds()
    {
        GameObject aCloud = (GameObject)Instantiate(clouds[Random.Range(0, clouds.Length)]);

        aCloud.transform.position = new Vector3(12f, Random.Range(0.3f, 2.7f), 20);


        scheduleNextSpawn();
    }
    void scheduleNextSpawn()
    {
        float spawnInSeconds;
        if (maxSpawnRateInSeconds > 1f)
        {
            spawnInSeconds = Random.Range(1f, maxSpawnRateInSeconds);
        }
        else
        {
            spawnInSeconds = 1f;
        }
        Invoke("SpawnClouds", spawnInSeconds);

    }

}
