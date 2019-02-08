using UnityEngine;

public class Audio : MonoBehaviour {

    private AudioSource source;

    public AudioClip correct;
    public AudioClip wrong;

    public float volume = .25f;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void wrongSound()
    {
        source.PlayOneShot(wrong, volume);
    }

    public void correctSound()
    {
        source.PlayOneShot(correct, volume);
    }
}
