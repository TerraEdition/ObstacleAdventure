using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFly : MonoBehaviour
{
    [SerializeField]
    private float maxY;

    private float startY;

    private void Start()
    {
        startY = 0f;
    }

    void Update()
    {
        if (startY < maxY)
        {
            transform.position =
                new Vector2(transform.position.x,
                    transform.position.y + Time.deltaTime);
            startY += Time.deltaTime;
        }
        else
        {
            transform.position =
                new Vector2(transform.position.x,
                    transform.position.y - Time.deltaTime);
            startY -= Time.deltaTime;
        }
    }
}
