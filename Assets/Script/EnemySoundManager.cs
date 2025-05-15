using UnityEngine;

public class EnemySoundManager : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("Enemy Spawn Sound")]
    public AudioClip spawnSound;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySpawnSound()
    {
        if (spawnSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(spawnSound);
        }
    }
}
