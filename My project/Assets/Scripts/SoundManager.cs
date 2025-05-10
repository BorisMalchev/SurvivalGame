using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance { get; set; }
    //Sound Effects
    public AudioSource dropItemSound;
    public AudioSource pickupItemSound;
    public AudioSource craftItemSound;
    public AudioSource chopSound;
    public AudioSource grassWalkSound;
    public AudioSource toolSwingSound;
    //BackgroundMusic
    public AudioSource startingZoneBGMusic;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    public void PlaySound(AudioSource soundToPlay)
    {
        if (soundToPlay.isPlaying == false)
        {
            soundToPlay.Play();
        }
    }
}
