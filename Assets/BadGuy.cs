using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BadGuy : MonoBehaviour
{

    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform target;

    float playerSightRange;
    private float recoveryTime = 0;
    [SerializeField] float wanderRange = 10;
    [SerializeField] float playerAttackRange = 5;

    [SerializeField] Transform wander4;
    float distance4;

    [SerializeField] MyStates m_States;

    enum MyStates
    { 
        WANDER,
        PURSUE,
        ATTACK,
        RECOVERY
    }

    // Update is called once per frame
    void Update()
    {
        playerSightRange = Vector3.Distance(transform.position, target.position);

        switch (m_States)
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
        if (gameObject.GetComponent<Rigidbody>() != null)
        {
            Destroy(gameObject.GetComponent<Rigidbody>());
        }

        agent.SetDestination(wander4.position);

        if (agent.SetDestination(wander4.position) == true)
        {
            distance4 = Vector3.Distance(transform.position, wander4.position);
            if (distance4 < 1f)
            {
                wander4.position = new Vector3(Random.Range(-7.5f, 18), -.27f, Random.Range(15, 48));
            }
        }

        if (playerSightRange <= wanderRange)
        {
            m_States = MyStates.PURSUE;
        }
    }

    void UpdatePursue()
    {
        agent.SetDestination(target.position);

        if (playerSightRange <= playerAttackRange)
        {
            m_States = MyStates.ATTACK;
        }

        if (playerSightRange > wanderRange)
        {
            m_States = MyStates.WANDER;
        }
    }

    void UpdateAttack()
    {
        if (gameObject.GetComponent<Rigidbody>() == null)
        {
            gameObject.AddComponent<Rigidbody>();
        }
        Attack();
    }

    void UpdateRecovery()
    {
        if (gameObject.GetComponent<Rigidbody>() != null)
        {
            Destroy(gameObject.GetComponent<Rigidbody>());
        }

        recoveryTime += Time.deltaTime;

        if (recoveryTime > 5f)
        {
            recoveryTime = 0;
            m_States = MyStates.WANDER;
        }
    }

    private void Attack()
    { 
        Vector3 direction = (target.position - transform.position).normalized;
        GetComponent<Rigidbody>().AddForce(direction, ForceMode.Impulse);
        Debug.Log("Enemy has attacked!");

        if (playerSightRange >= playerAttackRange)
        {
            m_States = MyStates.PURSUE;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("Player has taken damage!");
            m_States = MyStates.RECOVERY;
        }
    }
}
