using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject _freezeProjectil;
    public Transform _firePointTansform;
    public float _fireSpeed;
    private PowerBar _poweBar;

    private CharacterController _characterController;
    private bool _balanced;
    [SerializeField]
    private float _slideFriction = 0.3f;
    private Vector3 _hitNormal;

    private float _timeSlipTillDie = 0.5f;
    private float _timeStartSlip;

    private Vector3 _moveDirection;
    private float _gravity = 20f;
    private float _verticalVelocity;
    private PlayerActions _actionsAnimation;
    private float _nextAttack;
    private AudioControl _audioControl;
    private ShootableThing _shootableThing;
    private bool _isDead;
    private GetTerrainTexture _getTerrainTexture;

    public GameObject _cameraTarget;
    public float _attackRate;
    public float _walkSpeed;
    public float _runSpeed;
    public float _jumpForce;
    public GameObject _soldier; // o boneco
    

    void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _actionsAnimation = GetComponentInChildren<PlayerActions>();
        _audioControl = GetComponentInChildren<AudioControl>();
        _shootableThing = GetComponent<ShootableThing>();
        _getTerrainTexture = GetComponent<GetTerrainTexture>();
        _poweBar = GameObject.FindGameObjectWithTag("powerBar").GetComponent<PowerBar>();
    }    

    // Update is called once per frame
    void Update()
    {
        if (_shootableThing.IsDead())
        {
            Die();
        }
        else
        {
            MovePlayer();
            Attack();           
        }
    }

    private void FixedUpdate()
    {
        Spell();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //_charIsColliding = true;
        _hitNormal = hit.normal;
        
    }   

    void Attack()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > _nextAttack)
        {            
             _nextAttack = Time.time + _attackRate;
             _actionsAnimation.Attack1();
             
        }

        if (Input.GetButtonDown("Fire2") && Time.time > _nextAttack)
        {
            _nextAttack = Time.time + _attackRate;
            _actionsAnimation.Attack2();
            
        }        
    }

    void Spell()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Fire();
            _actionsAnimation.Defend(true);
        }
        else
        {
            _actionsAnimation.Defend(false);
        }
    }

    void MovePlayer()
    {        
        _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f,
                                     Input.GetAxis("Vertical"));

        _moveDirection = transform.TransformDirection(_moveDirection);
        if(_moveDirection != Vector3.zero)
        {
           _soldier.transform.rotation = Quaternion.LookRotation(_moveDirection);
        }        

        _moveDirection *= _runSpeed * Time.deltaTime;
        
        FootStepSound(1f);       
        
        ApplyGravity();

        //escorregar se nao estiver dentro do agulo limite
        if (!_balanced && _characterController.isGrounded)
        {            
            SlipMoviment();
        }
        else
        {
            _timeStartSlip = Time.time;
        }

        _characterController.Move(_moveDirection);

        //verificar se es em equilibrio
        _balanced = Vector3.Angle(Vector3.up, _hitNormal) <= _characterController.slopeLimit;


        if (_characterController.velocity.magnitude < 0.3f)
        {
            _actionsAnimation.Stay();
        }
        else
        {
            _actionsAnimation.Run();
        }      
    }

    private void SlipMoviment()
    {
        if(Time.time - _timeStartSlip >= _timeSlipTillDie)
        {
            _shootableThing.Damage(100);
        }
        _actionsAnimation.Stay();
        _moveDirection.x += (1f - _hitNormal.y) * _hitNormal.x * (_walkSpeed - _slideFriction);
        _moveDirection.z += (1f - _hitNormal.y) * _hitNormal.z * (_walkSpeed - _slideFriction);
    }

    void ApplyGravity()
    {
        _verticalVelocity -= _gravity * Time.deltaTime;
        PlayerJump();
        _moveDirection.y = _verticalVelocity * Time.deltaTime;
    }

    void PlayerJump()
    {
        if(_characterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            _verticalVelocity = _jumpForce;
            //_actionsAnimation.Jump();
        }
    }

    private void FootStepSound(float speed)
    {
        if(_characterController.isGrounded == true && _characterController.velocity.sqrMagnitude > 0.3f)
        {
            _audioControl.PlayAudioWalk(speed);
        }
    }

    public void Die()
    {
        if (!_isDead)
        {
            _characterController.enabled = false;
            _jumpForce = 0;
            _actionsAnimation.Dizzy();
            _isDead = true;
        }
    }

    private void Fire()
    {
        if (_poweBar.UsePower(10))
        {
            GameObject projectil = Instantiate(_freezeProjectil, _firePointTansform.position, _cameraTarget.transform.rotation);
            projectil.GetComponent<Rigidbody>().AddForce(projectil.transform.forward * _fireSpeed);
            Destroy(projectil, 120f);
        }
    }

    public void CollectPower(int power)
    {
        _poweBar.AddPower(power);
    }
}
