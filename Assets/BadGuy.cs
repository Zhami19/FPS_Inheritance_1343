using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BadGuy : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    [SerializeField] NavMeshAgent agent;

    [SerializeField] Transform target;

    [SerializeField] MyStates m_States;

    float wanderRange;
    Vector3 startingLocation;
    float playerSightRange;
    float playerAttackRange = 2;
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

        if (playerSightRange <= 10)
        {
            m_States = MyStates.PURSUE;
        }
    }

    void UpdatePursue()
    {
        agent.SetDestination(target.position);

        if (playerSightRange <= 5)
        {
            m_States = MyStates.ATTACK;
        }

        if (playerSightRange > 10)
        {
            m_States = MyStates.WANDER;
        }
    }

    void UpdateAttack()
    {     
        if (playerSightRange > 10)
        {
            if (gameObject.GetComponent<Rigidbody>() != null)
            {
                Destroy(gameObject.GetComponent<Rigidbody>());
            }
            m_States = MyStates.WANDER;
        }
        else if (playerSightRange > 5)
        {
            if (gameObject.GetComponent<Rigidbody>() != null)
            {
                Destroy(gameObject.GetComponent<Rigidbody>());
            }
            m_States = MyStates.PURSUE;
        }
        else if (playerSightRange <= 3)
        {
            gameObject.AddComponent<Rigidbody>();
            Attack();
        }
    }

    void UpdateRecovery()
    {
        var time = 0f;
        time += Time.deltaTime;

        if (time > 5f)
        {
            m_States = MyStates.WANDER;
        }
    }

    private void Attack()
    {
        //Vector3 currTarget = new Vector3(target.position.x, target.position.y, target.position.z);
        Vector3 direction = (target.position - transform.position).normalized;
        GetComponent<Rigidbody>().AddForce(direction * 3, ForceMode.Impulse);
        Debug.Log("Enemy has attacked!");
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("triggered");
        if (other.tag == "Player")
        {
            Debug.Log("Player has taken damage!");
            m_States = MyStates.RECOVERY;
        }
    }
}
