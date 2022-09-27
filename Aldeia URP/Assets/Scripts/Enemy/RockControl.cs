using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockControl : MonoBehaviour
{
    private Rigidbody _rigBody;

    private void Awake()
    {
        _rigBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _rigBody.isKinematic = true;
        _rigBody.useGravity = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _rigBody.isKinematic = false;
            _rigBody.useGravity = true;


            Destroy(gameObject, 60f);
        }
    }
    
}
