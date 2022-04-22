using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    private LayerMask explodeAble;

    [SerializeField]
    private GameObject explode;

    Animator anim;

    bool canExplode = true;


    CircleCollider2D coll;

    private void Awake()
    {
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if (IsGround() && canExplode)
        {
            canExplode = false;
            anim.SetBool("Ground", true);
            StartCoroutine(Explode());
        }
        if (transform.position.y < -8f)
        {
            Destroy (gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Trap")
        {
            InstantiateExplode();
        }
    }

    void InstantiateExplode()
    {
        Instantiate(explode, transform.position, Quaternion.identity);
        Destroy (gameObject);
    }

    private bool IsGround()
    {
        return Physics2D
            .BoxCast(coll.bounds.center,
            coll.bounds.size,
            0f,
            Vector2.down,
            .1f,
            explodeAble);
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(3f);
        InstantiateExplode();
    }
}
