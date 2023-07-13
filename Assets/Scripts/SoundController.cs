using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    #region Singleton
    public static SoundController sharedInstance;

    void Awake()
    {
        sharedInstance = this;
        if (this != sharedInstance)
        {
            Debug.Log("Warning! More than 1 instance of SoundController has been detected");
        }
    }
    #endregion

    void Start()
    {
        playBGM();
    }
    public AudioSource SFXSource;
    public AudioSource VFXSource;
    public AudioSource BGMSource;

    [Header("SFXSounds")]
    public AudioClip buttonClick;
    public AudioClip selectFoodSound;
    public AudioClip eatButtonSound;

    [Header("VFXSounds")]
    public AudioClip fallingCerealSound;
    public AudioClip fallingMilkSound;
    public AudioClip eatingCerealSound;

    [Header("BGMSound")]
    public AudioClip BGMSound;
    public void playSFX(AudioClip clip, bool isLoop)
    {
        if (SFXSource.clip != null) SFXSource.Stop();

        SFXSource.clip = clip;

        SFXSource.loop = isLoop;

        SFXSource.Play();
    }

    public void playVFX(AudioClip clip, bool isLoop)
    {
        if (VFXSource.clip != null) VFXSource.Stop();

        VFXSource.clip = clip;

        VFXSource.loop = isLoop;

        VFXSource.Play();
    }

    public void playBGM()
    {
        if (BGMSource.clip != null) BGMSource.Stop();

        BGMSource.clip = BGMSound;
        BGMSource.loop = true;
        BGMSource.Play();
    }

    public void stopBGM()
    {
        BGMSource.Stop();
    }

    public void OnButtonClick()
    {
        playSFX(buttonClick, false);
    }
}
