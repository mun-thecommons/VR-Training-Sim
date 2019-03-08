using UnityEngine;

public class Audio : MonoBehaviour {

    private AudioSource source;

    public AudioClip correct;
    public AudioClip wrong;
    public AudioClip loginToLinux;
    public AudioClip registerForMath1000;

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


    public void loginToLinuxSound()
    {
        source.PlayOneShot(loginToLinux, volume);
    }

    public void registerForMath1000Sound()
    {
        source.PlayOneShot(registerForMath1000, volume);
    }


}
