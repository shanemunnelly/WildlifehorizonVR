using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class MonkeyMovement : MonoBehaviour
{
    GameObject player;
    NavMeshAgent agent;

    [SerializeField] float sightRange = 10f;
    [SerializeField] float animalSpeed = 5f;
    [SerializeField] float stoppingDistance = 1.5f;

    bool playerInSight;
    Animator animationController;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animationController = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");

        agent.speed = animalSpeed;
    }

    void Update()
    {
        playerInSight = Vector3.Distance(transform.position, player.transform.position) <= sightRange;

        if (playerInSight)
        {
            agent.SetDestination(player.transform.position);

            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (distanceToPlayer <= stoppingDistance)
            {
                agent.velocity = Vector3.zero;
                agent.isStopped = true;
                animationController.SetBool("isRunning", false);
            }
            else
            {
                agent.isStopped = false;
                agent.speed = animalSpeed;
                animationController.SetBool("isRunning", true);
            }
        }
        else
        {
            // Perform other behaviors like patrolling or fleeing when the player is out of sight range
            Patrol();
        }
    }

    void Patrol()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            SearchForDest();
        }
    }

    void SearchForDest()
    {
        float range = 10f; // Adjust this value as needed
        float z = Random.Range(-range, range);
        float x = Random.Range(-range, range);

        Vector3 destPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(destPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
