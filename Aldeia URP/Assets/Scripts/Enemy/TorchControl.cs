using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchControl : MonoBehaviour
{
    public ParticleSystem _smokeParticle;
    public ParticleSystem _fireParticle;

    private SphereCollider _sphereCollider;

    private void Awake()
    {
        _sphereCollider = GetComponent<SphereCollider>();
    }
    public void Freeze()
    {
        var main = _smokeParticle.main;
        main.maxParticles = 1;
        main.loop = false;

        _fireParticle.Pause(true);

        _sphereCollider.enabled = false;
        
    }

    public void Unfreeze()
    {
        var main = _smokeParticle.main;
        main.maxParticles = 1000;
        main.loop = true;
        _smokeParticle.Play();

        _fireParticle.Play();

        _sphereCollider.enabled = true;

    }
}
