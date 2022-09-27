using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public int _gunDamage;
    public float _fireRate;
    public float _weaponRange;
    public float _hitForce;
    public Transform gunEnd;
    public Sprite _crosshair;

    private Camera _mainCamera;
    private WaitForSeconds _shotDuration = new WaitForSeconds(1f);     //usado para otempo de duracao do laiser
    private WaitForSeconds _timeAimingAnimate = new WaitForSeconds(10f);
    private AudioSource _gunAudio;
    private LineRenderer _laserLine;
    private float _nextFire;    
    private PlayerActions _actions;
    




    // Start is called before the first frame update
    void Awake()
    {
        _laserLine = GetComponentInChildren<LineRenderer>();
        _gunAudio = GetComponent<AudioSource>();
        _mainCamera = FindObjectOfType<Camera>();
        _actions = GameObject.FindGameObjectWithTag("soldier").GetComponent<PlayerActions>();
    }    



    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(_mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f)),
                        _mainCamera.transform.forward * 100f, Color.green);

        
            
            

        if (Input.GetButtonDown("Fire1") && Time.time > _nextFire)
        {
            _nextFire = Time.time + _fireRate;

            //Start our ShotEffect coroutine to turn our laser line on and off
            StartCoroutine(ShotEffect());
            Vector3 rayOrigin = _mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;
            _laserLine.SetPosition(0, gunEnd.position);
            if (Physics.Raycast(rayOrigin, _mainCamera.transform.forward, out hit, _weaponRange))
            {
                _laserLine.SetPosition(1, hit.point);

                // Get a reference to a health script attached to the collider we hit
                ShootableThing health = hit.collider.GetComponent<ShootableThing>();
                
                if (health != null)
                {
                    health.Damage(_gunDamage);
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * _hitForce);
                }
            }
            else
            {
                _laserLine.SetPosition(1, rayOrigin + (_mainCamera.transform.forward * _weaponRange));
            }
            //_actions.Attack();
        }
    }

    public Sprite getCrosshair()
    {
        return _crosshair;
    }

    private IEnumerator AimGun()
    {
        //_actions.Aiming();
        yield return _timeAimingAnimate;
        _actions.Stay();
    }

    private IEnumerator ShotEffect()
    {
        _gunAudio.Play();
        _laserLine.enabled = true;

        //WaitForEndOfFrame for 0.07 seconds
        // yield return retoma esta rotina depois de executar a rotina shotDuration,
        //ou seja retorna aqui passado 7 milisegundos
        yield return _shotDuration;
        _laserLine.enabled = false;
    }
}
