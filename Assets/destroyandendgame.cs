using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyandendgame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        var enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            Destroy(collision.gameObject);
        }
    }
}
