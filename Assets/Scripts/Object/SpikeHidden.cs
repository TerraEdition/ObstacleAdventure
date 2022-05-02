using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHidden : MonoBehaviour
{
    [SerializeField]
    private GameObject changePrefab;

    [SerializeField]
    private GameObject startPrefab;

    [SerializeField]
    private float startBtwTime;

    [SerializeField]
    private float endBtwTime;

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
        }
        lastPrefab = (GameObject) Instantiate(changePrefab);
        yield return new WaitForSeconds(startBtwTime);
        StartCoroutine(EndPrefab());
    }

    IEnumerator EndPrefab()
    {
        if (lastPrefab != null)
        {
            Destroy (lastPrefab);
        }
        lastPrefab = (GameObject) Instantiate(startPrefab);
        yield return new WaitForSeconds(endBtwTime);
        StartCoroutine(StartPrefab());
    }
}
