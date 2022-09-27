using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    
    public Transform _target, _player;
    
    public float _sensitive = 1;
    [SerializeField]
    private Vector2 _lockLimits = new Vector2(-35, 60);
    private Vector2 _mouse;

    private Transform _obstruction;
    private float _zoomSpeed = 2f; 

    // Start is called before the first frame update
    void Start()
    {
        _obstruction = _target;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        CamControl();
       //ViewObstructed();   // esta configurado so para mashRenderer.. 
    }

    void CamControl()
    {
        _mouse.x += Input.GetAxis("Mouse X") * _sensitive;
        _mouse.y -= Input.GetAxis("Mouse Y") * _sensitive;
        _mouse.y = Mathf.Clamp(_mouse.y, _lockLimits.x, _lockLimits.y);

        transform.LookAt(_target);

        if (Input.GetKey(KeyCode.Q))
        {
            _target.rotation = Quaternion.Euler(_mouse.y, _mouse.x, 0f);
        }
        else
        {
            _target.rotation = Quaternion.Euler(_mouse.y, _mouse.x, 0f);

            if(!_player.GetComponent<ShootableThing>().IsDead())
                _player.rotation = Quaternion.Euler(0f, _mouse.x, 0f);
        }
    }

    void ViewObstructed()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, _target.position - transform.position, out hit, 4.5f))
        {
            if(hit.collider.gameObject.tag != "Player")
            {
                _obstruction = hit.transform;
                _obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                if(Vector3.Distance(_obstruction.position, transform.position) >= 3f &&Vector3.Distance(transform.position, _target.position) >= 1.5f)
                {
                    transform.Translate(Vector3.forward * _zoomSpeed * Time.deltaTime);
                }
            }
            else
            {
                _obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                if(Vector3.Distance(transform.position, _target.position) < 4.5f)
                {
                    transform.Translate(Vector3.back * Time.deltaTime);
                }
            }
        }
    }
}
