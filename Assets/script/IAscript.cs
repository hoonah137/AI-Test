using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAscript : MonoBehaviour
{
    //Script state machine

    enum State 
    {
        Patrolling,
        Chasing
    }

    State _currentState;

    NavMeshAgent _enemyAgent;

    Transform _playerTransform;

    [SerializeField] Transform patrolAreaCenter;

    [SerializeField] Vector2 areaSize;

    [SerializeField] float visionRange = 15;

    [SerializeField] float visionAngle = 90;


    void Awake ()
    {
        _enemyAgent = GetComponent<NavMeshAgent>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Start()
    {
        _currentState = State.Patrolling;
    }

    // Update is called once per frame
    void Update()
    {
        switch (_currentState)
        {
            case State.Patrolling:
                Patrol();
            break;

            case State.Chasing:
                Chase();
            break;
        }
    }

    void Patrol()
    {
        if(OnRange() == true)
        {
            _currentState = State.Chasing;
        }
        if (_enemyAgent.remainingDistance < 0.5f)
        {
            SetRandomPoint();
        }
    }

    void Chase()
    {
        _enemyAgent.destination = _playerTransform.position;

        if(OnRange() == false)
        {
            _currentState = State.Patrolling;
        }
    }

    void SetRandomPoint()
    {
        float _randomX = Random.Range(-areaSize.x /2, areaSize.x/2);
        float _randomZ = Random.Range(-areaSize.y /2, areaSize.y/2);

        Vector3 randomPoint = new Vector3(_randomX, 0f, _randomZ) + patrolAreaCenter.position;
        _enemyAgent.destination = randomPoint;

    }

    bool OnRange()
    {
        /*
        if (Vector3.Distance(transform.position, _playerTransform.position) <= visionRange)
        {
            return true;
        }

        return false;
        */
        
        Vector3 _directionToPlayer = _playerTransform.position - transform.position;
        float _distanceToPlayer = _directionToPlayer.magnitude;
        float _angleToPlayer = Vector3.Angle(transform.forward,_directionToPlayer);

        if (_distanceToPlayer <= visionRange && _angleToPlayer <visionAngle * 0.5f)
        {return true;}

        return false;

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(patrolAreaCenter.position, new Vector3(areaSize.x, 0, areaSize.y));

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);

        Gizmos.color = Color.green;
        Vector3 fovLine1 = Quaternion.AngleAxis(visionAngle *0.5f, transform.up) * transform.forward * visionRange;
        Vector3 fovLine2 = Quaternion.AngleAxis(-visionAngle *0.5f, transform.up) * transform.forward * visionRange;
        Gizmos.DrawLine(transform.position, transform.position + fovLine1);
        Gizmos.DrawLine(transform.position, transform.position + fovLine2);

    }
}

/* {
    Pino >>>>> Tetas
}*/