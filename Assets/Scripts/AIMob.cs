using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMob : MonoBehaviour
{
    public GameObject target; // cible du mob
    public float distance; // gestion du déplacement de l'ennemi
    public float attack = 25;
    public float speed =2.5f;
    public int speedMove = 7;
    public bool dead = false; // gestion de la mort du mob


    void Start()
    {
        
    }

    void Update()
    {
         distance = Vector2.Distance(transform.position, target.transform.position);
         if(distance<attack)
         {
              Attack();
         } 
    }

    // On déplace l'objet vers sa target (target = joueur)
    void Attack()
    {
        transform.position = Vector2.Lerp(transform.position, target.transform.position, speed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Triggr de l'objet en collision : " + collision.gameObject.name);

    }


    IEnumerator hit(GameObject col)
    {
        
        yield return new WaitForSeconds(1);
    }

}
