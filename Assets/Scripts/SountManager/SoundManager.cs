using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("audioSources")]
    public AudioSource music_Source; // loa điều khiển nhạc nền
    public AudioSource sfx_Source;   // loa điều khiển âm thanh hiệu ứng

    [Header("audioClip")]
    public AudioClip win_sound;
    public AudioClip fail_sound;
    public AudioClip correct_sound;
    public AudioClip Uncorrect_sound;
    public AudioClip click_sound;
    public AudioClip clock_sound;
    public AudioClip grmusic_sound;
    public AudioClip coid_sound;
    public AudioClip open_sound;
    public static SoundManager instance;

    public float volumMussic;
    public float volumUI;
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(this);

        music_Source.volume = PrefManager.PrefProfiles.getValueMussic();
        sfx_Source.volume = PrefManager.PrefProfiles.getValueUI();
    }
     
    public void setVolumMussic(float value) // hàm thay đổi âm thanh
    {
        volumMussic = value;
        music_Source.volume = value;
        PrefManager.PrefProfiles.SetVolumMussic(value);
    }    
    public void setVolumUI(float value)
    {
        volumUI = value;
        sfx_Source.volume = value;
        PrefManager.PrefProfiles.SetVolumUI(value);
    }

    public void startmussicGr()// hàm phát nhạc nền
    {
        music_Source.clip = grmusic_sound;
        music_Source.loop = true;
        music_Source.Play();
    }    

    public void pauseMussicGr()
    {
        music_Source.Pause();
    } 
    
    public void UnpauseMussicGR()
    {
        music_Source.UnPause();
    }    
    public void stopMussicGr()
    {
        music_Source.Stop();
    }    
    public void playOpenSound()
    {
        sfx_Source.PlayOneShot(open_sound);
    }    
    public void playWinSound()
    {
        sfx_Source.PlayOneShot(win_sound);
    }    
    public void playFailSound()
    {
        sfx_Source.PlayOneShot(fail_sound);
    }    
    public void playClickSound()
    {
        sfx_Source.PlayOneShot(click_sound);
    }    
    public void playClockSound()
    {
        sfx_Source.PlayOneShot(clock_sound);
    }    
    public void stopClockSound()
    {
        sfx_Source.Stop();
    }    
    public void playCorrectSound()
    {
        sfx_Source.PlayOneShot(correct_sound);
    }    
    public void playUncorrectSound()
    {
        sfx_Source.PlayOneShot(Uncorrect_sound);
    }    
    public void playCoinSound()
    {
        sfx_Source.PlayOneShot(coid_sound);
    }    
}
