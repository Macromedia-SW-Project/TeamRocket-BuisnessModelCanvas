using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SetVariables : MonoBehaviour
{
    static public float SpeedReference = 2;
    static public int HealthReference = 5;
    public int health;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpeedReference = speed; HealthReference = health;
    }

    public void NewEnemy()
    {
        
    }
}
