using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MaintenanceAudio : MonoBehaviour
{
    void Start()
    {
        AudioSource audio = GetComponent<AudioSource>();
        if (audio != null && !audio.isPlaying)
        {
            audio.loop = true;
            audio.Play();
        }
    }
}
