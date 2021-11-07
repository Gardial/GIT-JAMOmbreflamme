using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    [Range(1, 20)]
    public float RangeAtk;
    public int health;
    public int damage;
    [Range(0, 5)]
    public float distanceFromEnemy;
    [Range(1, 5)]
    public float invincibleTime;

    private List<GameObject> lstBehindEnemies; // Enemies behind
    private List<GameObject> lstForwardEnemies; // Enemies forward

    private bool isInvincible;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private float lastTime;

    private bool continueUpdate = true;

    public GameObject gameOver;

    private AudioSource audioSource;

    public GameObject hearts;

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

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health > 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && Time.time != lastTime)
            {
                lastTime = Time.time;
                spriteRenderer.flipX = true;
                animator.SetTrigger("Attack");
            }

            if (Input.GetKeyDown(KeyCode.RightArrow) && Time.time != lastTime)
            {
                lastTime = Time.time;
                spriteRenderer.flipX = false;
                animator.SetTrigger("Attack");
            }

            if (lstBehindEnemies.Count > 0 && lstBehindEnemies[0] == null)
            {
                lstBehindEnemies.RemoveAt(0);
            }

            if (lstForwardEnemies.Count > 0 && lstForwardEnemies[0] == null)
            {
                lstForwardEnemies.RemoveAt(0);
            }

            spriteRenderer.color = Color.black;

        }
        else if(continueUpdate)
        {
            animator.SetTrigger("Death");
            continueUpdate = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject != null && !isInvincible)
        {
            health -= collision.gameObject.GetComponent<Enemy>().damage;
            if(health >= 0)
            {
                GameObject.Destroy(hearts.transform.GetChild(health).gameObject);
            }
            isInvincible = true;
            animator.SetTrigger("TakeHit");
            StartCoroutine(WaitSeconds());
        }
    }

    public void TeleportPlayer()
    {
        if(spriteRenderer.flipX == true)
        {
            if(lstBehindEnemies.Count > 0 && lstBehindEnemies[0] != null)
            {
                transform.position = new Vector2(lstBehindEnemies[0].transform.position.x + distanceFromEnemy, transform.position.y);
            }

        }
        else
        {
            if (lstForwardEnemies.Count > 0 && lstForwardEnemies[0] != null)
            {
                transform.position = new Vector2(lstForwardEnemies[0].transform.position.x - distanceFromEnemy, transform.position.y);
            }
        }
    }

    public void DealDamage()
    {
        if (spriteRenderer.flipX == true)
        {
            if (lstBehindEnemies.Count > 0 && lstBehindEnemies[0] != null)
            {
                lstBehindEnemies[0].gameObject.GetComponent<Enemy>().health -= damage;
            }

        }
        else
        {
            if (lstForwardEnemies.Count > 0 && lstForwardEnemies[0] != null)
            {
                lstForwardEnemies[0].gameObject.GetComponent<Enemy>().health -= damage;
            }
        }
    }

    public void DeleteInList(GameObject enemy)
    {
        if (transform.position.x > enemy.transform.position.x)
        {
            lstBehindEnemies.RemoveAt(0);
        }
        else if (transform.position.x < enemy.transform.position.x)
        {
            lstForwardEnemies.RemoveAt(0);
        }
    }

    IEnumerator WaitSeconds()
    {
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
    }

    public void GameOver()
    {
        gameObject.SetActive(true);
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
