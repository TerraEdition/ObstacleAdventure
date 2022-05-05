using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    // [HideInInspector]
    public bool isDie = false;

    private void Update()
    {
        if (transform.position.y < -8f)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Rigidbody2D>().velocity +=
                new Vector2(0, 5f);
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!isDie)
            {
                other.gameObject.GetComponent<Die>().Death();
            }
        }
    }

    private void Die()
    {
        isDie = true;
        GetComponent<Rigidbody2D>().gravityScale = 1;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().flipY = true;
        Vector3 move =
            new Vector3(Random.Range(40, 70), Random.Range(-40, 40), 0);
        transform.position += move * Time.deltaTime;
    }
}
