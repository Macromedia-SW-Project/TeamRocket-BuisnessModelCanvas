using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public GameObject MeshObject;
    public AudioSource CoinSound;
    bool collected = false;

    ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {   
        scoreManager = GameObject.Find("ScoreCanvas").GetComponent<ScoreManager>();
    }

   private void OnTriggerEnter(Collider other)
   {
        if (collected)
        {
            return;
        }
    if(other.CompareTag("Player"))
    {
        scoreManager.IncreaseScore();
        MeshObject.SetActive(false);
            CoinSound.Play();
            collected = true; 
    }
   }
}
