using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    [Range(1, 10)]
    public float RangeAtk;
    public int health;
    public int damage;
    [Range(1, 5)]
    public float invincibleTime;

    public Material material;
    public GameObject gameOver;

    private List<GameObject> lstBehindEnemies; // Enemies behind
    private List<GameObject> lstForwardEnemies; // Enemies forward

    private bool isInvincible;

    private Vector2 targetPosition;
    private GameObject target;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private float lastTime;

    // Start is called before the first frame update
    void Start()
    {
        lstBehindEnemies = transform.GetChild(0).GetComponent<DetectEnnemies>().lstMobs;
        lstForwardEnemies = transform.GetChild(1).GetComponent<DetectEnnemies>().lstMobs;

        transform.GetChild(0).GetComponent<BoxCollider2D>().size = new Vector2(RangeAtk, 5);
        transform.GetChild(0).GetComponent<BoxCollider2D>().offset = new Vector2(-RangeAtk/2, 0);

        transform.GetChild(1).GetComponent<BoxCollider2D>().size = new Vector2(RangeAtk, 5);
        transform.GetChild(1).GetComponent<BoxCollider2D>().offset = new Vector2(RangeAtk/2, 0);

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health > 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && Time.time != lastTime && lstBehindEnemies.Count > 0 && lstBehindEnemies[0] != null)
            {
                lastTime = Time.time;
                targetPosition = new Vector2(lstBehindEnemies[0].transform.position.x, transform.position.y);
                target = lstBehindEnemies[0];
                spriteRenderer.flipX = true;
                animator.SetTrigger("Attack");
            }

            if (Input.GetKeyDown(KeyCode.RightArrow) && Time.time != lastTime && lstForwardEnemies.Count > 0 && lstForwardEnemies[0] != null)
            {
                targetPosition = new Vector2(lstForwardEnemies[0].transform.position.x, transform.position.y);
                target = lstForwardEnemies[0];
                spriteRenderer.flipX = false;
                animator.SetTrigger("Attack");
            }
        }

        else
        {
            gameOver.SetActive(true);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject != null && !isInvincible)
        {
            health -= collision.gameObject.GetComponent<Enemy>().damage;
            isInvincible = true;
            //StartCoroutine(BecomeTemporarilyInvincible());
        }
    }

    private IEnumerator BecomeTemporarilyInvincible()
    {
        isInvincible = true;

        for (float i = 0; i < invincibleTime; i += 0.15f)
        {
            // Alternate between 0 and 1 scale to simulate flashing
            if (material.color.r > 0)
            {
                material.color = Vector4.zero;
            }
            else
            {
                material.color = new Vector4(255, 0, 0, 255);
            }
            yield return new WaitForSeconds(0.15f);
        }

        material.color = Vector4.zero;
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
    }

    public void TeleportPlayer()
    {
        if(transform.position.x > targetPosition.x)
        {
            transform.position = new Vector2(targetPosition.x + 1f, targetPosition.y);
        }
        else
        {
            transform.position = new Vector2(targetPosition.x - 1f, targetPosition.y);
        }
    }

    public void DealDamage()
    {
        if(target.GetComponent<Enemy>() != null)
        {
            target.GetComponent<Enemy>().health -= damage;
        }
    }

    public void DeleteInList(GameObject enemy)
    {
        if (enemy.GetComponent<Enemy>().health <= 0 && transform.position.x > enemy.transform.position.x)
        {
            lstBehindEnemies.RemoveAt(0);
        }
        else if (enemy.GetComponent<Enemy>().health <= 0 && transform.position.x < enemy.transform.position.x)
        {
            lstForwardEnemies.RemoveAt(0);
        }
    }
}
