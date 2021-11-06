using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("Trashbin").transform.position.x < transform.position.x)
        {
            transform.position = new Vector2(transform.position.x - 0.01f, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x + 0.01f, transform.position.y);
        }
    }
}
