using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonSmokeAttack : MonoBehaviour
{
    public int _damage;
    public float _attackRate;

    private float _nextAttack;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Time.time > _nextAttack)
        {
            _nextAttack = Time.time + _attackRate;
            other.GetComponent<ShootableThing>().Damage(_damage);
        }
    }
}
