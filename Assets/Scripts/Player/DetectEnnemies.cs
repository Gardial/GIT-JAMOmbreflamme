using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnnemies : MonoBehaviour
{
    public List<GameObject> lstMobs;
  
    private void Start()
    {
        lstMobs = new List<GameObject>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != null)
        {
            lstMobs.Add(collision.gameObject);
        }
    }
}
