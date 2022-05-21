using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    //AudiClips
    public static AudioClip click, ski, boom_1,boom_2,boom_3, coinCollect, woshIn,woshOut,pop,eagle,bonusLevel,boxCollect,taDa,chaChing;

    public AudioSource soundEffects;
    public AudioSource mainMusic;
    public AudioSource skiSound;
    public AudioSource drumsSound;

    //Static audiosources
    static AudioSource soundEffectsAudioSrc;
    static AudioSource mainMusicAudioSrc;
    static AudioSource skiAudioSrc;
    static AudioSource drumsSoundSrc;

    //Volumes
    public static float musicVolume;
    public static float soundEffectVolume;
    public static float skiVolume;

    public static bool isPlay;
    public static bool isFadeOut;

    void Start()
    {
        //Load clips
        click = Resources.Load<AudioClip>("Click");
        ski = Resources.Load<AudioClip>("Ski");
        boom_1 = Resources.Load<AudioClip>("Boom");
        boom_2 = Resources.Load<AudioClip>("Boom_2");
        boom_3 = Resources.Load<AudioClip>("Boom_3");
        coinCollect = Resources.Load<AudioClip>("Coin sound");
        woshIn = Resources.Load<AudioClip>("Wosh in");
        woshOut = Resources.Load<AudioClip>("Wosh out");
        pop = Resources.Load<AudioClip>("Pop");
        eagle = Resources.Load<AudioClip>("Eagle");
        bonusLevel = Resources.Load<AudioClip>("Bonus level");
        boxCollect = Resources.Load<AudioClip>("Box collect");
        taDa = Resources.Load<AudioClip>("Ta da");
        chaChing = Resources.Load<AudioClip>("Cha ching");

        //initilazi sources
        soundEffectsAudioSrc = soundEffects;
        mainMusicAudioSrc = mainMusic;
        skiAudioSrc = skiSound;
        drumsSoundSrc = drumsSound;

        isFadeOut = false;

        musicVolume = 1f;
        soundEffectVolume = 0.5f;
        skiVolume = 1f;

        mainMusicAudioSrc.volume = musicVolume / 4;
        soundEffectsAudioSrc.volume = soundEffectVolume;
        skiAudioSrc.volume = skiVolume;
    }

    void Update()
    {
        if(isFadeOut)
        {
            skiAudioSrc.volume -= 4 * Time.deltaTime;
            if (skiAudioSrc.volume <= 0)
            {
                isFadeOut = false;
                skiAudioSrc.volume = skiVolume;
                skiAudioSrc.Stop();
            }
        }
        else
            skiAudioSrc.volume = skiVolume;

        if(isPlay)
        {
            mainMusicAudioSrc.volume = musicVolume;
        }
        else
        {
            mainMusicAudioSrc.volume = musicVolume / 4;
        }
        soundEffectsAudioSrc.volume = soundEffectVolume;
    }

    public static void PlaySound(string name)
    {

        switch (name)
        {
            case "Click":
                soundEffectsAudioSrc.PlayOneShot(click);
                break;
            case "Ski":
                isFadeOut = false;
                skiAudioSrc.volume = skiVolume;
                skiAudioSrc.Stop();
                skiAudioSrc.PlayOneShot(ski);
                break;
            case "Boom":
                int a = Random.Range(1, 4);
                switch(a)
                {
                    case 1:
                        soundEffectsAudioSrc.PlayOneShot(boom_1);
                        break;
                    case 2:
                        soundEffectsAudioSrc.PlayOneShot(boom_2);
                        break;
                    case 3:
                        soundEffectsAudioSrc.PlayOneShot(boom_3);
                        break;
                }
                break;
            case "Coin":
                soundEffectsAudioSrc.PlayOneShot(coinCollect);
                break;
            case "Wosh in":
                soundEffectsAudioSrc.PlayOneShot(woshIn);
                break;
            case "Wosh out":
                soundEffectsAudioSrc.PlayOneShot(woshOut);
                break;
            case "Pop":
                soundEffectsAudioSrc.PlayOneShot(pop);
                break;
            case "Eagle":
                soundEffectsAudioSrc.PlayOneShot(eagle);
                break;
            case "Bonus level":
                soundEffectsAudioSrc.PlayOneShot(bonusLevel);
                break;
            case "Box collect":
                soundEffectsAudioSrc.PlayOneShot(boxCollect);
                break;
            case "Drums":
                drumsSoundSrc.Play();
                break;
            case "Ta da":
                soundEffectsAudioSrc.PlayOneShot(taDa);
                break;
            case "Cha ching":
                soundEffectsAudioSrc.PlayOneShot(chaChing);
                break;
        }
    }

    public static void StopSkiSound()
    {
        skiAudioSrc.Stop();
    }

    public static void StopDrums()
    {
        drumsSoundSrc.Stop();
    }



}
