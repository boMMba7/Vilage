using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpownEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _enemys;


    private BoxCollider _boxCollider;
    
    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            foreach (GameObject e in _enemys)
            {
                Vector3 spownPosition = RandomPointInBounds(_boxCollider.bounds);
                
                Instantiate(e, spownPosition, other.transform.rotation);
                
                yield return new WaitForSeconds(Random.Range(0f, 2f));
                
            }
        }
        _boxCollider.enabled = false;
    }


    Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            bounds.min.y,
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

}
