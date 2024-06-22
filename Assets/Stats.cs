using Course.PrototypeScripting;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Stats : MonoBehaviour
{
    private NavMeshAgent enemyspeed;
    public int hp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake()
    {
       enemyspeed = GetComponent<NavMeshAgent>();
       enemyspeed.speed = SetVariables.SpeedReference;
       hp = SetVariables.HealthReference;
    }
}
