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

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(3, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("Player").transform.position.x < transform.position.x)
        {
            rb.velocity = new Vector2(-speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(speed, 0);
        }

        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
