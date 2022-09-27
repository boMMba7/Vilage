using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    //***Arsenal control, codigo do criador do avatar soldado
    public Transform _rightGunBone;
    public Transform _leftGunBone;
    public Arsenal[] _arsenal;
    private Animator _animator;
    private int _curentGunIndex = 0;

    
    
    [System.Serializable]
    public struct Arsenal
    {
        public string name;
        public GameObject rightGun;
        public GameObject leftGun;
        public RuntimeAnimatorController controller;               
    }

    void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        if (_arsenal.Length > 0)
            SetArsenal(_arsenal[_curentGunIndex].name);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _curentGunIndex = 0;
            SetArsenal(_arsenal[_curentGunIndex].name);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _curentGunIndex = 1;
            SetArsenal(_arsenal[_curentGunIndex].name);
        }
    }

    public void SetArsenal(string name)
    {
        foreach (Arsenal hand in _arsenal)
        {
            if (hand.name == name)
            {
                if (_rightGunBone.childCount > 0)
                    Destroy(_rightGunBone.GetChild(0).gameObject);
                if (_leftGunBone.childCount > 0)
                    Destroy(_leftGunBone.GetChild(0).gameObject);
                if (hand.rightGun != null)
                {
                    GameObject newRightGun = (GameObject)Instantiate(hand.rightGun);
                    newRightGun.transform.parent = _rightGunBone;
                    newRightGun.transform.localPosition = Vector3.zero;
                    newRightGun.transform.localRotation = Quaternion.Euler(90, 0, 0);
                }
                if (hand.leftGun != null)
                {
                    GameObject newLeftGun = (GameObject)Instantiate(hand.leftGun);
                    newLeftGun.transform.parent = _leftGunBone;
                    newLeftGun.transform.localPosition = Vector3.zero;
                    newLeftGun.transform.localRotation = Quaternion.Euler(90, 0, 0);
                }
                _animator.runtimeAnimatorController = hand.controller;
                return;
            }
        }
    }

    public Animator GetCurentAnimator()
    {
        return _animator;
    }
}
