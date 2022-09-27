using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusCatched : MonoBehaviour
{
    public int _health;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<ShootableThing>().AddHealth(_health);
            Destroy(gameObject);
        }
    }
    

}
