using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnim : MonoBehaviour
{
    Animator _anim;

    void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    public void Walk()
    {
        _anim.SetFloat("speed", 1f);
    }

    public void Attack ()
    {
        _anim.SetFloat("speed", 0f);
        _anim.SetTrigger("attack1");
    }

    public void Idle ()
    {
        _anim.SetFloat("speed", 0f);
    }

    public void Dead()
    {
        _anim.SetTrigger("die");
    }

    
}
