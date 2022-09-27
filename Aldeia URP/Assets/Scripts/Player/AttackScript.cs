using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public int _damage;

    void OnCollisionEnter(Collision col)
    {        
        if(col.gameObject.CompareTag("Enemy"))
        {
            col.gameObject.GetComponent<ShootableThing>().Damage(_damage);            
        }

        if (col.gameObject.CompareTag("box"))
        {
            col.gameObject.GetComponent<BonusBox>().getHit();
        }
    }
}
