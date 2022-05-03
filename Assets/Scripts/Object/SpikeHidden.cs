using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHidden : MonoBehaviour
{
    [SerializeField]
    private GameObject secondPrefab;

    [SerializeField]
    private GameObject firstPrefab;

    [SerializeField]
    private float startTime;

    [SerializeField]
    private float BtwTime;

    private GameObject lastPrefab;

    void Start()
    {
        StartCoroutine(StartPrefab());
    }

    IEnumerator StartPrefab()
    {
        if (lastPrefab != null)
        {
            Destroy (lastPrefab);
            lastPrefab =
                (GameObject)
                Instantiate(firstPrefab,
                transform.position,
                Quaternion.identity);
            yield return new WaitForSeconds(BtwTime);
        }
        else
        {
            lastPrefab =
                (GameObject)
                Instantiate(firstPrefab,
                transform.position,
                Quaternion.identity);
            yield return new WaitForSeconds(startTime);
        }
        StartCoroutine(EndPrefab());
    }

    IEnumerator EndPrefab()
    {
        Destroy (lastPrefab);
        lastPrefab =
            (GameObject)
            Instantiate(secondPrefab, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(BtwTime);
        StartCoroutine(StartPrefab());
    }
}
