using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SetVariables : MonoBehaviour
{
    public GameObject CopiedEnemy;
    static public float SpeedReference = 2;
    static public int HealthReference = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(CopiedEnemy, transform.position, Quaternion.identity);
        }
    }

    public void SpawnEnemy()
    {
        Instantiate(CopiedEnemy, transform.position, Quaternion.identity);
    }
    public void Speedy()
    {
        SpeedReference = 4;
        HealthReference = 7;
    }
    public void Normal()
    {
        SpeedReference = 2;
        HealthReference = 10;
    }
    public void Boss()
    {
        SpeedReference = 1;
        HealthReference = 100;
    }
}
