using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Die : MonoBehaviour
{
    Rigidbody2D rb;

    Animator anim;

    private bool canDie = true;

    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = AudioManager.instance;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (transform.position.y < -8f && canDie)
        {
            canDie = false;
            Death();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Trap")
        {
            Death();
        }
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.gameObject.tag == "Enemy")
    //     {
    //         Death();
    //     }
    // }
    public void Death()
    {
        AdsManager.instance.countAds();
        GetComponent<BoxCollider2D>().enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
        audioManager.PlaySound("Die");
        GameManager.instance.heart -= 1;
        PlayerPrefs.SetInt("heart", GameManager.instance.heart);
        GameManager.instance.updateHeart = true;
        GameManager.instance.playerDie = true;
    }

    void Restart()
    {
        //     GameManager.instance.coinTempo = 0;
        //     GameManager.instance.jewelTempo = 0;
        //     GameManager.instance.key = 0;
        //     GameManager.instance.finished = false;
        //     SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Destroy (gameObject);
    }
}
