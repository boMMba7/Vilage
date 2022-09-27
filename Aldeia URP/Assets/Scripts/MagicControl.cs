using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicControl : MonoBehaviour
{
    private SphereCollider _sphereCollider;




    private void OnTriggerEnter(Collider other)
    {        
        //to iguinore the sphere collider
        if(other is BoxCollider)
        {
            MagicEffect collisionMagicEffect = other.gameObject.GetComponent<MagicEffect>();

            if (collisionMagicEffect != null)
            {
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                collisionMagicEffect.Freeze();
                Destroy(gameObject);
            }
            else
            {
                if (!other.gameObject.CompareTag("Player"))
                    Destroy(gameObject);
            }
        }
    }
}
