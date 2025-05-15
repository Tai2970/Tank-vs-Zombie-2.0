using UnityEngine;

public class GhostSoundManager : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("Ghost Sound (played once on spawn)")]
    public AudioClip ghostSound;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayGhostSound()
    {
        if (ghostSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(ghostSound);
        }
    }
}
