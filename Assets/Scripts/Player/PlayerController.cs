using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    [Range(1, 5)]
    public float RangeAtk;
    public int health;

    public float smooth;

    private List<GameObject> lstBehindEnemies; // Enemies behind
    private List<GameObject> lstForwardEnemies; // Enemies forward

    // Start is called before the first frame update
    void Start()
    {
        lstBehindEnemies = transform.GetChild(0).GetComponent<DetectEnnemies>().lstMobs;
        lstForwardEnemies = transform.GetChild(1).GetComponent<DetectEnnemies>().lstMobs;

        transform.GetChild(0).GetComponent<BoxCollider2D>().size = new Vector2(RangeAtk, 3);
        transform.GetChild(0).GetComponent<BoxCollider2D>().offset = new Vector2(-RangeAtk/2, -1);

        transform.GetChild(1).GetComponent<BoxCollider2D>().size = new Vector2(RangeAtk, 3);
        transform.GetChild(1).GetComponent<BoxCollider2D>().offset = new Vector2(RangeAtk/2, -1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && lstBehindEnemies.Count > 0)
        {
            transform.position = new Vector2(lstBehindEnemies[0].transform.position.x, transform.position.y);
            Destroy(lstBehindEnemies[0]);
            lstBehindEnemies.Remove(lstBehindEnemies[0]);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && lstForwardEnemies.Count > 0)
        {
            transform.position = new Vector2(lstForwardEnemies[0].transform.position.x, transform.position.y);
            Destroy(lstForwardEnemies[0]);
            lstForwardEnemies.Remove(lstForwardEnemies[0]);
        }
    }
}
