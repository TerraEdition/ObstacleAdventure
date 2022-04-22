using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    Rigidbody2D rb;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = Random.Range(1f, 1.5f);
        StartCoroutine(duration());
    }

    void FixedUpdate()
    {
        Vector2 direction = new Vector2(-1f, 0f);
        float inputMagnitude = Mathf.Clamp01(direction.magnitude);
        direction.Normalize();
        transform.Translate(direction * speed * inputMagnitude * Time.deltaTime, Space.World);
    }
    // Update is called once per frame

    IEnumerator duration()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }

}
