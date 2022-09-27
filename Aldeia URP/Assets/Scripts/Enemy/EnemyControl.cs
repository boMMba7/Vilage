using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public enum EnemyState
{
    PATROL,
    CHASE,
    ATTACK,
    FREEZE
}

public class EnemyControl : MonoBehaviour
{    
    public float _fieldOfViewAngle;
    public float _attackRate;
    public int _damage;
    public AudioManager _audioManager;

    private float _nextAttack;
    private GameObject _player;
    //private bool _playerInSight;
    private SphereCollider _collider;
    private Vector3 _lastKnowPlayerCord;
    private EnemyAnim _enemyAnim;
    private NavMeshAgent _navAgent;
    public float _attackDistance;
    private EnemyState _enemyState;
    private float _patrolRadiosMin = 20f, _patrolRadiUsMax = 60f;
    private ShootableThing _health;
    private AudioControl _audioControl;
    private ShootableThing _playerHealth;

    
    public int _powerReward = 5;


    void Awake()
    {
        _collider = GetComponent<SphereCollider>();
        _enemyAnim = GetComponent<EnemyAnim>();
        _navAgent = GetComponent<NavMeshAgent>();
        
        _health = GetComponent<ShootableThing>();
        _audioControl = GetComponent<AudioControl>();

        _player = GameObject.FindWithTag("Player");
        _playerHealth = _player.GetComponent<ShootableThing>();
    }

    // Start is called before the first frame update
    void Start()
    {    
        _enemyState = EnemyState.PATROL;
    }

    // Update is called once per frame
    void Update()
    {
        if (_health.IsDead())
        {
            Die();
        }
        else
        {
            if (_enemyState == EnemyState.PATROL || _playerHealth.IsDead())
            {
                Patrol();
            }
            else if(_enemyState == EnemyState.CHASE)
            {
                Chase();
            }
            else if (_enemyState == EnemyState.ATTACK)
            {
                Attack();
            }
            else if (_enemyState == EnemyState.FREEZE)
            {
                Freeze();
            }
        }
    }

    private void LateUpdate()
    {
        if(_enemyState == EnemyState.ATTACK || _enemyState == EnemyState.FREEZE)
        {
            _navAgent.isStopped = true;
        }
        else
        {
            _navAgent.isStopped = false;
            _enemyAnim.Walk();
        }
    }

    void WalkSound()
    {
        if(_navAgent.velocity.sqrMagnitude > 0.2f)
        {
            _audioControl.PlayAudioWalk();
        }
    }

    void SetNewRandomDestination()
    {
        float randRadius = Random.Range(_patrolRadiosMin, _patrolRadiUsMax);
        Vector3 randDir = Random.insideUnitSphere * randRadius;
        randDir += transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDir, out navHit, randRadius, -1);
        _navAgent.SetDestination(navHit.position);
        
    }

    void OnTriggerStay(Collider other)
    {
        if (_enemyState != EnemyState.ATTACK &&_enemyState != EnemyState.FREEZE && other.gameObject == _player && !_playerHealth.IsDead())
        {
            //_playerInSight = false;
            //verifica o angolo entre os vetores do inimigo a olhar para frente
            //e o vetor do inimigo para o objecto
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);
            
            if (angle < _fieldOfViewAngle * 0.5f)       //se estiver dentro do campo de visao
            {
               // _playerInSight = true;
                _lastKnowPlayerCord = _player.transform.position;
                _enemyState = EnemyState.CHASE;
            }
            else               //se sair dentro do campo de visao
            {
                _enemyState = EnemyState.PATROL;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _player && _enemyState == EnemyState.ATTACK && _enemyState != EnemyState.FREEZE)
        {            
            //_navAgent.isStopped = false;
            _enemyState = EnemyState.PATROL;
            //_playerInSight = false;
        }
    }

    void Chase()
    {
        _navAgent.SetDestination(_lastKnowPlayerCord);
        //verificar se ha algum obstaculo entre o inimigo e objecto
        RaycastHit hit;
        Vector3 origem = (transform.position + transform.up * 0.5f);
        if (Physics.Raycast(origem, transform.forward, out hit, _collider.radius))
        {
            //se o obstaculo for o jogador e tiver ao alcance do ataque
            if (hit.collider.gameObject == _player && PlayerOnRange() )
            {
                //_navAgent.isStopped = true;
                _enemyState = EnemyState.ATTACK;
            }
            else
            {
                _enemyState = EnemyState.CHASE;
            }         
        }
        WalkSound();
    }

    float dist;
    //verdade se o player estiver dentro do alcance de ataque
    bool PlayerOnRange()
    {
        dist = Vector3.Distance(_player.transform.position, transform.position) ;
        return dist < _attackDistance;
    }
    
    void Patrol()
    {
        if (HasAgentReachedDestination() )
        {
           
            SetNewRandomDestination();
        }
        WalkSound();
    }

    private bool HasAgentReachedDestination()
    {
        //case is not spawned or to far from mesh
        if (!_navAgent.isOnNavMesh)
        {
            return false;
        }

        if (!_navAgent.pathPending)
        {
            if(_navAgent.remainingDistance <= _navAgent.stoppingDistance)
            {
                if (!_navAgent.hasPath || _navAgent.velocity.sqrMagnitude <= 0f)
                {
                    return true;
                }
            }
        }
        return false;
    }

    void Attack()
    {
        if ( Time.time > _nextAttack && !_playerHealth.IsDead())
        {
            _nextAttack = Time.time + _attackRate;
            _playerHealth.Damage(_damage);
            _enemyAnim.Attack();
        }

        if (!PlayerOnRange())
            _enemyState = EnemyState.CHASE;
    }

    public void Freeze()
    {
        if (!_health.IsDead())
        {
            _enemyState = EnemyState.FREEZE;
            _navAgent.isStopped = true;
            _navAgent.velocity = Vector3.zero;
            _enemyAnim.Idle();
        }
        
    }

    public void UnFreeze()
    {
        if (!_health.IsDead())
        {
            _navAgent.isStopped = false;
            _enemyState = EnemyState.PATROL;
        }
    }

    public void Die()
    {
        _player.GetComponent<PlayerControl>().CollectPower(_powerReward);

        _audioControl.PlayAudioDie();

        _navAgent.velocity = Vector3.zero;
        _navAgent.isStopped = true;
        _navAgent.enabled = false;

        GetComponent<SphereCollider>().enabled = false;

        _enemyAnim.Dead();

        enabled = false;

        GetComponent<MagicEffect>().DestroyIce(5);
        Destroy(gameObject, 5);
    }

}
