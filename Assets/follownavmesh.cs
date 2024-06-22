using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class follownavmesh : MonoBehaviour
{
    [SerializeField] private Transform EnemyEndGoal;
    
    private NavMeshAgent navMeshAgent;
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        navMeshAgent.destination = EnemyEndGoal.position;
    }
}
