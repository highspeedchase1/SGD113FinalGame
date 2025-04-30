using UnityEngine;

public class AudioController : MonoBehaviour
{
    AudioSource src;

    private void Awake()
    {
        src = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        //Change the auseio that is currently being played
        src.clip = clip;

        //Play the new audio
        src.Play();
    }
}
