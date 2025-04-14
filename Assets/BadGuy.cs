using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BadGuy : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;

    [SerializeField] Transform target;

    [SerializeField] MyStates m_States;

    float wanderRange;
    Vector3 startingLocation;
    float playerSightRange;
    float playerAttackRange;
    float currentStateElapsed;
    float recoveryTime;

    [SerializeField] Transform wander4;
    float distance4;

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
        if (agent.SetDestination(wander4.position) == true)
        {
            distance4 = Vector3.Distance(transform.position, wander4.position);
            if (distance4 < 1f)
            {
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
