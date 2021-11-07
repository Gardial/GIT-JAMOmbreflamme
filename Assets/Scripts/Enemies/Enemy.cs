using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Settings")]
    [Range(1, 10)]
    public float speed;

    [Range(1, 3)]
    public int health;

    [Range(1, 5)]
    public int damage;

    private Rigidbody2D rb;

    private PlayerController playerController;

    private Animator animator;

    private bool continueUpdate;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(3, 3);

        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        continueUpdate = true;

        spriteRenderer = GetComponent<SpriteRenderer>();

        if(transform.position.x < playerController.gameObject.transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(health > 0)
        {
            if(GameObject.Find("Player").transform.position.x < transform.position.x)
            {
                rb.velocity = new Vector2(-speed, 0);
            }
            else
            {
                rb.velocity = new Vector2(speed, 0);
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        if(health <= 0 && continueUpdate)
        {
            playerController.DeleteInList(gameObject);
            animator.SetTrigger("Death");
            continueUpdate = false;
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
