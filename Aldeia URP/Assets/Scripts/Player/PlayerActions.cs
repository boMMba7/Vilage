using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private Animator _characterController;

    public BoxCollider _espadaCollider;

    private void Awake()
    {
        _characterController = GetComponent<Animator>();
    }

    public void Stay()
    {
        _characterController.SetFloat("speed", 0f);
    }

    public void SetSpeed(float speed)
    {
        _characterController.SetFloat("speed", speed);
    }

    public void Walk()
    {
        _characterController.SetFloat("speed", 1f);
        
    }

    public void Run()
    {
        _characterController.SetFloat("speed", 2f);
    }

    public void Attack1()
    {
        _characterController.SetTrigger("attack1");
    }

    public void Attack2()
    {
        _characterController.SetTrigger("attack2");
    }

    public void Defend(bool b)
    {
        _characterController.SetBool("defend", b);
    }

    public void Dizzy()
    {
        _characterController.SetTrigger("dizzy");
    }

    public void Roll(bool b)
    {
        _characterController.SetBool("roll", b);
    }

    //vai ser chamado num determinado frame da animacao attack
    void OnAttackScript()
    {
        _espadaCollider.enabled = true;
    }

    //vai ser chamado num determinado frame da animacao attack
    void OffAttackScript()
    {
        _espadaCollider.enabled = false;
    }   
}
