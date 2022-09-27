using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBox : MonoBehaviour
{
    private Animator _boxAnimator;

    public GameObject _fullBox;
    public GameObject _destroyedBox;
    public GameObject _bonus;

    private void Awake()
    {
        _boxAnimator = GetComponentInChildren<Animator>();
    }

    public void getHit()
    {
        GetComponent<BoxCollider>().enabled = false;
        _fullBox.SetActive(false);
        _destroyedBox.SetActive(true);
        _boxAnimator.SetTrigger("bang");
        Instantiate(_bonus, _destroyedBox.transform.position, Quaternion.Euler(new Vector3(0, 0,0)));
        Destroy(gameObject, 60f);
    }

}
