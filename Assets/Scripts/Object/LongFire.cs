using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongFire : MonoBehaviour
{
    [SerializeField]
    float t = 0.01f;

    [SerializeField]
    float duration = 3f;

    [SerializeField]
    bool positiveRotate;

    private Quaternion target;

    private void Update()
    {
        if (positiveRotate)
        {
            target =
                transform.rotation * Quaternion.AngleAxis(45f, Vector3.forward);
        }
        else
        {
            target =
                transform.rotation *
                Quaternion.AngleAxis(-45f, Vector3.forward);
        }
        transform.rotation =
            Quaternion
                .Lerp(transform.rotation,
                target,
                t + Time.deltaTime / duration);
    }
}
