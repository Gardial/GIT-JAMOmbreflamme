using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArms : MonoBehaviour
{
    public List<GameObject> lstMobs;
  
    private void Start()
    {
        lstMobs = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Mob")
        {
            lstMobs.Add(collision.gameObject);
        }
    }
}
