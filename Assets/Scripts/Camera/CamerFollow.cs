using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerFollow : MonoBehaviour
{
    private float length, startpos;
    public GameObject player;
    public float smooth;
    public float parallaxEffect;

    private Vector2 velocity;

    private void Start()
    {
        //startpos = transform.position.x;
        //if (!GameObject.Find("Main Camera"))
        //{
        //    length = GetComponent<SpriteRenderer>().bounds.size.x;
        //}
    }

    private void Update()
    {
        //float temp = (transform.position.x * (1 - parallaxEffect));
        //float dist = (transform.position.x * parallaxEffect);

        //if (temp > startpos + length) startpos += length;
        //else if (temp < startpos - length) startpos -= length;
    }

    void LateUpdate()
    {

        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smooth);
        transform.position = new Vector3(posX, transform.position.y, transform.position.z);
    }
}
