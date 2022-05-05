using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAxe : MonoBehaviour
{
    [SerializeField]
    float t = 0.01f;

    [SerializeField]
    float duration = 3f;

    public bool positiveRotate;

    Vector2 _direction;

    [SerializeField]
    private float speed = 2f;

    private bool isReady = false;

    private Quaternion target;

    void Rotate()
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

    private void Update()
    {
        Rotate();
        if (isReady)
        {
            Vector2 position = transform.position;
            position += _direction * speed * Time.deltaTime;
            transform.position = position;

            if (transform.position.y < -8f)
            {
                Destroy (gameObject);
            }
            // Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
            // Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
            // if (
            //     (transform.position.x < min.x) ||
            //     (transform.position.y < min.y) ||
            //     (transform.position.x > max.x) ||
            //     (transform.position.y > max.y)
            // )
            // {
            //     Destroy (gameObject);
            // }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Trap" || other.gameObject.tag == "Ground")
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Trap" || other.gameObject.tag == "Ground")
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction.normalized;
        isReady = true;
    }
}
