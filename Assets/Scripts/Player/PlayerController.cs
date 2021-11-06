using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private List<GameObject> lstLeftMobs; //Liste des ennemis bras gauche
    private List<GameObject> lstRightMobs; // Liste des ennemis bras droit

    void Start()
    {
        lstLeftMobs = transform.GetChild(0).GetComponent<AttackArms>().lstMobs; 
        lstRightMobs = transform.GetChild(1).GetComponent<AttackArms>().lstMobs; 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && lstLeftMobs.Count > 0 && Input.GetKeyDown(KeyCode.RightArrow)== false)
        {
            Destroy(lstLeftMobs[0]);
            lstLeftMobs.Remove(lstLeftMobs[0]);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && lstRightMobs.Count > 0 && Input.GetKeyDown(KeyCode.LeftArrow) == false)
        {
            Destroy(lstRightMobs[0]);
            lstRightMobs.Remove(lstRightMobs[0]);
        }
    }


}

