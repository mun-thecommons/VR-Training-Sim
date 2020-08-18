using UnityEngine;
/// <summary>
/// Houses all Audio clips used within the Game
/// 
/// ##Detailed
/// This script contains the logic which plays the correct audio sounds within the game. The different functions are referenced throughout other scripts
/// after certain tasks have been completed. Tha audio clips themselves are assigned from within the Unity program.
/// </summary>
public class Audio : MonoBehaviour {

    private AudioSource source;

    public AudioClip touchVest;
    public AudioClip correct;
    public AudioClip wrong;
    public AudioClip loginToLinux;
    public AudioClip registerForMath1000;
    public AudioClip noVideo;
    public AudioClip noFiles;
    public AudioClip balanceNegative;
    public AudioClip noLogin;
    public AudioClip noEmail;
  
    public float volume = .25f;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }
    /********************
     * Function associated with audioclip
     * 
     * This is the function used within other scripts to play the associated audioclip
     * 
     * *****************/
    public void wrongSound()
    {
        source.PlayOneShot(wrong, volume);
    }
    /********************
     * Function associated with audioclip
     * 
     * This is the function used within other scripts to play the associated audioclip
     * 
     * *****************/
    public void correctSound()
    {
        source.PlayOneShot(correct, volume);
    }
    /********************
     * Function associated with audioclip
     * 
     * This is the function used within other scripts to play the associated audioclip
     * 
     * *****************/
    public void loginToLinuxSound()
    {
        source.PlayOneShot(loginToLinux, volume);
    }
    /********************
     * Function associated with audioclip
     * 
     * This is the function used within other scripts to play the associated audioclip
     * 
     * *****************/
    public void registerForMath1000Sound()
    {
        source.PlayOneShot(registerForMath1000, volume);
    }
    /********************
     * Function associated with audioclip
     * 
     * This is the function used within other scripts to play the associated audioclip
     * 
     * *****************/
    public void noVideoSound()
    {
        source.PlayOneShot(noVideo, volume);
    }
    /********************
     * Function associated with audioclip
     * 
     * This is the function used within other scripts to play the associated audioclip
     * 
     * *****************/
    public void noFilesSound()
    {
        source.PlayOneShot(noFiles, volume);
    }
    /********************
     * Function associated with audioclip
     * 
     * This is the function used within other scripts to play the associated audioclip
     * 
     * *****************/
    public void balanceNegativeSound()
    {
        source.PlayOneShot(balanceNegative, volume);
    }
    /********************
     * Function associated with audioclip
     * 
     * This is the function used within other scripts to play the associated audioclip
     * 
     * *****************/
    public void noLoginSound()
    {
        source.PlayOneShot(noLogin, volume);
    }
    /********************
     * Function associated with audioclip
     * 
     * This is the function used within other scripts to play the associated audioclip
     * 
     * *****************/
    public void noEmailSound()
    {
        source.PlayOneShot(noEmail, volume);
    }
    /********************
     * Function associated with audioclip
     * 
     * This is the function used within other scripts to play the associated audioclip
     * 
     * *****************/
    public void touchVestSound()
    {
        source.PlayOneShot(touchVest, volume);
    }
}
