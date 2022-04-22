using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePrefab : MonoBehaviour
{
    public GameObject[] prefabs;

    [SerializeField]
    private float[] coordY;

    [SerializeField]
    private float[] coordX;

    [SerializeField]
    private float minSpawnRateInSeconds = 3f;

    [SerializeField]
    private float maxSpawnRateInSeconds = 3f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnObject", maxSpawnRateInSeconds);
    }

    void SpawnObject()
    {
        GameObject aCloud =
            (GameObject) Instantiate(prefabs[Random.Range(0, prefabs.Length)]);

        aCloud.transform.position =
            new Vector3(Random.Range(coordX[0], coordX[1]),
                Random.Range(coordY[0], coordY[1]),
                0);
        scheduleNextSpawn();
    }

    void scheduleNextSpawn()
    {
        Invoke("SpawnObject",
        Random.Range(minSpawnRateInSeconds, maxSpawnRateInSeconds));
    }
}
