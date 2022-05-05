using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody2D rb;

    private float speed;

    [SerializeField]
    private GameObject explode;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = Random.Range(3f, 5f);
    }

    void FixedUpdate()
    {
        Vector2 direction = new Vector2(-1f, 0f);
        float inputMagnitude = Mathf.Clamp01(direction.magnitude);
        direction.Normalize();
        transform
            .Translate(direction * speed * inputMagnitude * Time.deltaTime,
            Space.World);
        if (transform.position.x < -40f)
        {
            Destroy (gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            InstantiateExplode();
        }
    }

    void InstantiateExplode()
    {
        Instantiate(explode, transform.position, Quaternion.identity);
        Destroy (gameObject);
    }
}
