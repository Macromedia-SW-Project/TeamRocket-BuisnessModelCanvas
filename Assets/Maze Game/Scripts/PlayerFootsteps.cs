using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerFootsteps : MonoBehaviour
{
    public AudioClip[] footstepSounds; // Array von Schrittgeräuschen
    public float stepInterval = 0.5f; // Zeitintervall zwischen den Schritten
    public float movementThreshold = 0.1f; // Schwellenwert für die Bewegungserkennung

    private AudioSource audioSource;
    private CharacterController characterController;
    private Rigidbody rigidbody;
    private float stepTimer;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        characterController = GetComponent<CharacterController>();
        rigidbody = GetComponent<Rigidbody>();
        stepTimer = 0f;
    }

    void Update()
    {
        if (IsMoving())
        {
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f)
            {
                PlayFootstepSound();
                stepTimer = stepInterval;
            }
        }
        else
        {
            stepTimer = 0f; // Reset timer when player stops
        }
    }

    private bool IsMoving()
    {
        bool isMoving = false;

        if (characterController != null)
        {
            isMoving = characterController.velocity.magnitude > movementThreshold;
        }

        if (!isMoving && rigidbody != null)
        {
            isMoving = rigidbody.velocity.magnitude > movementThreshold;
        }

        return isMoving;
    }

    private void PlayFootstepSound()
    {
        if (footstepSounds.Length > 0)
        {
            int index = Random.Range(0, footstepSounds.Length);
            audioSource.PlayOneShot(footstepSounds[index]);
        }
    }
}
