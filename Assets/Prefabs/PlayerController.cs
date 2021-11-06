using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private List<GameObject> lstLeftMobs; //Liste des ennemis bras gauche
    private List<GameObject> lstRightMobs; // Liste des ennemis bras droit

    // Start is called before the first frame update
    void Start()
    {
        lstLeftMobs = transform.GetChild(0).GetComponent<AttackArms>().lstMobs;
        lstRightMobs = transform.GetChild(1).GetComponent<AttackArms>().lstMobs;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && lstLeftMobs.Count > 0)
        {
            Destroy(lstLeftMobs[0]);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && lstRightMobs.Count > 0)
        {
            Destroy(lstRightMobs[0]);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       /* if (collision.gameObject.tag == "Monstre")
        {
            lstLeftMobs.Add(collision.gameObject);
        } */
    }



}

