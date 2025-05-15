using UnityEngine;

public class GameResultSoundManager : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip resultSound; // Assign different clips for win/lose

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayResultSound()
    {
        if (audioSource != null && resultSound != null)
        {
            audioSource.PlayOneShot(resultSound);
        }
    }
}
