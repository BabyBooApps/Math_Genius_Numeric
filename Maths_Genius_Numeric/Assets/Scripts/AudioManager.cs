using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource[] Audio_Source_List;
    public AudioSource GameAudios;
    public AudioSource BG_audio;

    public float backgroundMusicVolume;
    public float SoundEffectsVolume;

    public AudioClip Btn_Click;
    public AudioClip Background_Music;
    public AudioClip Cheer_Clip;

    public List<AudioClip> FailClips = new List<AudioClip>();

    private void Awake()
    {
        // Ensure there is only one instance of this class
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Get_Audio_Source_List();
        SetAudioVolumes();
        Play_BAckground_Music();
    }



    public void Get_Audio_Source_List()
    {
        Audio_Source_List = Camera.main.GetComponents<AudioSource>();
        GameAudios = Audio_Source_List[0];
        BG_audio = Audio_Source_List[1];
    }

    public void SetAudioVolumes()
    {

        // Check if the key has been set
        if (PlayerPrefs.HasKey("BgSoundsON"))
        {
            if (PlayerPrefs.GetInt("BgSoundsON") == 1)
            {
                SetBgVolume(backgroundMusicVolume);
            }
            else
            {
                SetBgVolume(0);
            }
        }
        else
        {
            UI_Manager.instance.settings_Screen.On_BG_Music_On_Btn_Click();
        }


        if (PlayerPrefs.HasKey("SoundEffectsOn"))
        {
            if (PlayerPrefs.GetInt("SoundEffectsOn") == 1)
            {
                SetSoundEffectsVolume(SoundEffectsVolume);
            }
            else
            {
                SetSoundEffectsVolume(0);
            }
        }
        else
        {
            UI_Manager.instance.settings_Screen.On_Sound_Effects_On_Btn_Click();
        }

    }

    public void SetBgVolume(float vol)
    {
        BG_audio.volume = vol;
    }

    public void SetSoundEffectsVolume(float vol)
    {
        GameAudios.volume = vol;
    }

    public void Play_BAckground_Music()
    {
        BG_audio.Stop();
        BG_audio.clip = Background_Music;
        BG_audio.loop = true;
        BG_audio.Play();
    }

    public void Play_Game_Clip(AudioClip clip)
    {
        GameAudios.Stop();
        GameAudios.clip = clip;
        GameAudios.loop = false;
        GameAudios.Play();
    }

    public void Play_Btn_Click()
    {
        Play_Game_Clip(Btn_Click);
    }

    public void PlayFailClip()
    {
        AudioClip clip = FailClips.GetRandomElement();
        Play_Game_Clip(clip);
    }

    public void Play_Cheer_Clip()
    {
        Play_Game_Clip(Cheer_Clip);
    }




}
