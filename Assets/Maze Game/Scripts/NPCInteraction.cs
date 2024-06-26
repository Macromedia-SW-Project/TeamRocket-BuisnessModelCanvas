using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on NPC!");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collision with Player detected.");
            PlayCollisionSound();
        }
    }

    private void PlayCollisionSound()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            Debug.Log("Playing collision sound.");
            audioSource.Play();
        }
    }
}
