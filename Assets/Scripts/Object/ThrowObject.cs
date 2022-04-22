using System.Collections;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    private Transform target;

    public GameObject projectile;

    [SerializeField]
    private float durationShot = 2f;

    private SpriteRenderer sprite;

    private bool canShot = false;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (target != null)
        {
            if (target.transform.position.x >= transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            if (canShot)
            {
                canShot = false;
                StartCoroutine(Shot());
            }
        }
        else
        {
            canShot = false;
        }
    }

    IEnumerator Shot()
    {
        GameObject bullet = (GameObject) Instantiate(projectile);
        bullet.transform.position = transform.position;
        Vector2 direction =
            target.transform.position - bullet.transform.position;
        bullet.GetComponent<PickAxe>().SetDirection(direction);
        if (target.transform.position.x >= transform.position.x)
        {
            bullet.GetComponent<PickAxe>().positiveRotate = true;
        }
        else
        {
            bullet.GetComponent<PickAxe>().positiveRotate = false;
        }
        yield return new WaitForSeconds(durationShot);
        canShot = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = other.gameObject.transform;

            canShot = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            canShot = false;
        }
    }
}
