using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BadGuy : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;

    [SerializeField] Transform wander1;
    [SerializeField] Transform wander2;

    [SerializeField] Transform target;

    [SerializeField] MyStates m_States;

    float wanderRange;
    Vector3 startingLocation;
    float playerSightRange;
    float playerAttackRange;
    float currentStateElapsed;
    float recoveryTime;


    float distance1;
    float distance2;

    enum MyStates
    { 
        WANDER,
        PURSUE,
        ATTACK,
        RECOVERY
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerSightRange = Vector3.Distance(transform.position, target.position);

        switch(m_States)
        {
            case MyStates.WANDER:
                UpdateWander();
                break;
            case MyStates.PURSUE:
                UpdatePursue();
                break;
            case MyStates.ATTACK:
                UpdateAttack();
                break;
            case MyStates.RECOVERY:
                UpdateRecovery();
                break;
        }
    }

    void UpdateWander()
    {
        if (agent.SetDestination(wander1.position) == true)
        {
                if (distance1 < 1f)
                {
                    agent.SetDestination(wander2.position);
                    distance2 = Vector3.Distance(transform.position, wander2.position);
                }
        }

        if (agent.SetDestination(wander2.position) == true)
        {
                if (distance2 < 1f)
                {
                    agent.SetDestination(wander1.position);
                    distance1 = Vector3.Distance(transform.position, wander2.position);
                }
        }
    }

    void UpdatePursue()
    {
        agent.SetDestination(target.position);

        if (playerSightRange >= 5f)
        {
            agent.SetDestination(wander1.position);
            m_States = MyStates.WANDER;
        }

        /*if (playerSightRange <= 5f)
        {
            Debug.Log("Bad Guy has attacked");
            m_States = MyStates.;
        }*/

    }

    void UpdateAttack()
    {
        Debug.Log("Enemy has attacked!");
    }

    void UpdateRecovery()
    {

    }
}
