using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Collider enemyCheckCollider;

    private List<Enemy> detectedEnemies;
    private Enemy closestEnemy;

    // Start is called before the first frame update
    void Start()
    {
        detectedEnemies = new List<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        DrawDebugInformation();
        UpdateClosestEnemy();
        if (closestEnemy != null)
        {
            Debug.DrawLine(transform.position, closestEnemy.transform.position, Color.green);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            detectedEnemies.Add(enemy);
        }
        LogDetectedEnemies();
    }

    private void OnTriggerExit(Collider other)
    {
        var enemy = other.GetComponent<Enemy>();
        if (enemy && detectedEnemies.Contains(enemy))
        {
            detectedEnemies.Remove(enemy);
        }
        LogDetectedEnemies();
    }

    private void LogDetectedEnemies()
    {
        Debug.Log("================================");
        foreach(var e in detectedEnemies)
        {
            Debug.Log(e.gameObject.ToString());
        }
    }

    private void DrawDebugInformation()
    {
        foreach (var e in detectedEnemies)
        {
            Debug.DrawLine(transform.position, e.transform.position, Color.red);
        }
    }

    private void UpdateClosestEnemy()
    {
        if (detectedEnemies.Count <= 0)
        {
            closestEnemy = null;
            return;
        }

        // TODO: compare distances to tower and store closest one
        float closestDistance = 9999999999;
        foreach(Enemy e in detectedEnemies)
        {
            var offsetVector = new UnityEngine.Vector3(0.0f,0.0f,0.0f);
            offsetVector = transform.position - e.transform.position;
            float distance = offsetVector.magnitude;
            if (distance < closestDistance)
            {
                closestEnemy = e;
                closestDistance = distance;
            }
        }
    }
}
