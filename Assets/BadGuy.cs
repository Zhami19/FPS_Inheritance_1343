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


    [SerializeField] Transform wander3;
    [SerializeField] Transform wander4;

    float distance3;
    float distance4;

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
        wander3.position = new Vector3(Random.Range(-7.5f, 18), -.27f, Random.Range(15, 48));

        wander4.position = new Vector3(Random.Range(-7.5f, 18), -.27f, Random.Range(15, 48));

        agent.SetDestination(wander3.position);

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
        /*if (agent.SetDestination(wander1.position) == true)
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
                 distance1 = Vector3.Distance(transform.position, wander1.position);
            }
        }*/

        /*if (agent.SetDestination(wander3.position) == true)
        {
            distance3 = Vector3.Distance(transform.position, wander3.position);
            if (distance3 < 1f)
            {
                agent.SetDestination(wander4.position);
                Debug.Log("4: " + agent.SetDestination(wander4.position));
                distance4 = Vector3.Distance(transform.position, wander4.position);
                wander3.position = new Vector3(Random.Range(-7.5f, 18), -.27f, Random.Range(15, 48));
            }
        }*/

        if (agent.SetDestination(wander4.position) == true)
        {
            Debug.Log("4: " + agent.SetDestination(wander4.position));
            distance4 = Vector3.Distance(transform.position, wander4.position);
            if (distance4 < 1f)
            {
                agent.SetDestination(wander3.position);
                Debug.Log("3: " + agent.SetDestination(wander4.position));
                distance3 = Vector3.Distance(transform.position, wander3.position);
                wander4.position = new Vector3(Random.Range(-7.5f, 18), -.27f, Random.Range(15, 48));
            }
        }
    }

    void UpdatePursue()
    {
        /*agent.SetDestination(target.position);

        if (playerSightRange >= 5f)
        {
            *//*agent.SetDestination(wander1.position); //ORIGINAL TWO
            distance1 = Vector3.Distance(transform.position, wander2.position);*//*

           
            agent.SetDestination(wander3.position);
            distance3 = Vector3.Distance(transform.position, wander3.position);

            m_States = MyStates.WANDER;
        }*/

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
