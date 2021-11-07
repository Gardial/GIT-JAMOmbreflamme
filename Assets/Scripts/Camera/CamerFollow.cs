using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerFollow : MonoBehaviour
{
    public GameObject player;
    public float smooth;

    private Vector2 velocity;

    // Update is called once per frame
    void LateUpdate()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smooth);

        transform.position = new Vector3(posX, transform.position.y, transform.position.z);
    }
}
