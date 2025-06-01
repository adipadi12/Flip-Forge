using TMPro;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource; // Reference to the AudioSource component

    [SerializeField] private AudioClip collectingSound; // Array of audio clips to play
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private AudioClip gravitySwitchClip;

    public static AudioManager Instance { get; private set; } // Singleton instance
    private void Awake()
    {
        Instance = this; // Set the singleton instance
    }

    public void PlayCollectingSound()
    {
        audioSource.PlayOneShot(collectingSound); // Play the collecting sound
    }

    public void PlayDamageSound()
    {
        audioSource.PlayOneShot(damageSound); // Play the damage sound
    }

    public void PlayGravitySwitchSound()
    {
        audioSource.PlayOneShot(gravitySwitchClip); // Play the gravity switch sound
    }
}
