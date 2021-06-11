using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Panda;
using UnityEngine.AI;


public class AI : MonoBehaviour
{
    public NavMeshAgent playerNav;
    public Transform playerTransform;

    [Task]
    private bool detected;

    Vector3 playerLastPosition;

    private void Update()
    {
        Ray enemyRay = new Ray(transform.position, (playerTransform.position - transform.position).normalized);

        if(Physics.Raycast(enemyRay, out RaycastHit hit, 100) && hit.transform.CompareTag ("Player"))
        {
            detected = true;

            Debug.DrawRay(transform.position, (playerTransform.position - transform.position), Color.blue);

            playerLastPosition = playerTransform.position;
        }
        else
        {
            detected = false;

            Debug.DrawRay(transform.position, (playerTransform.position - transform.position), Color.red);
        }
            
    }

    [Task]
    private void ChasePlayer()
    {
        playerNav.SetDestination(playerLastPosition);
        if(Vector3.Distance(transform.position, playerLastPosition) < 0.05f)
        {
            Task.current.Succeed();
        }
    }

    [Task]
    private void watch()
    {
        playerNav.SetDestination(transform.position + Random.rotation * Vector3.forward * 5);
        Task.current.Succeed();
    }

}
