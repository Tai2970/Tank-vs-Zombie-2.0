using UnityEngine;

public class TankSoundManager : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip moveSound;
    public AudioClip shootSound;

    void Awake()
    {
        // Automatically find the AudioSource on the same GameObject
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMoveSound()
    {
        if (moveSound != null && audioSource != null && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(moveSound);
        }
    }

    public void PlayShootSound()
    {
        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }
}
