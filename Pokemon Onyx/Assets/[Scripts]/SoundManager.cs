using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class CustomAudio
{
    public AudioClip audioClip = null;
    public string name = "";

}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    //public Sound[] sounds;
    public List<CustomAudio> backgroundSoundClips;
    public List<CustomAudio> effectSoundClips;
    public CustomAudio aaa;

    public AudioSource bgm1;
    public AudioSource bgm2;
    public AudioSource UI;
    public AudioSource FX;

    //public AudioSource[] sounds;
    public List<AudioSource> soundSources;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        bgm1 = gameObject.AddComponent<AudioSource>();
        bgm2 = gameObject.AddComponent<AudioSource>();
        bgm1.volume = 0.0f;
        bgm2.volume = 0.0f;

        UI = gameObject.AddComponent<AudioSource>();
        FX = gameObject.AddComponent<AudioSource>();
        //foreach(var sound in sounds)
        //{
        //    sound.source = gameObject.AddComponent<AudioSource>();
        //    sound.source.clip = sound.clip;
        //    sound.source.volume = sound.volume;
        //    sound.source.pitch = sound.pitch;
        //    sound.source.loop = sound.loop;
        //}
        // master volume
        //AudioListener.volume = 0.02f;
       

        AudioClip[] sounds = Resources.LoadAll<AudioClip>("Sounds/BGM");

        foreach (AudioClip sound in sounds)
        {
            CustomAudio tmp = new CustomAudio();
            tmp.audioClip = sound;
            tmp.name = sound.name;
            backgroundSoundClips.Add(tmp);
        }

        sounds = Resources.LoadAll<AudioClip>("Sounds/FX");
        
        foreach(AudioClip sound in sounds)
        {
            CustomAudio tmp = new CustomAudio();
            tmp.audioClip = sound;
            tmp.name = sound.name;
            effectSoundClips.Add(tmp);
        }



    }


    public void PlayUI(string audioName)
    {
        UI.Stop();
        //CustomAudio sound = Array.Find(backgroundSoundClips, source => source.name == audioName);
        CustomAudio sound = backgroundSoundClips.Find(source => source.name == audioName);
        if (sound == null)
        {
            Debug.Log("Sound " + audioName + " is missing");
            return;
        }
        UI.clip = sound.audioClip;
        UI.loop = false;
        UI.Play();
    }

    public void PlayFX(string audioName)
    {
        FX.Stop();
        //CustomAudio sound = Array.Find(backgroundSoundClips, source => source.name == audioName);
        CustomAudio sound = backgroundSoundClips.Find(source => source.name == audioName);
        if (sound == null)
        {
            Debug.Log("Sound " + audioName + " is missing");
            return;
        }
        FX.clip = sound.audioClip;
        FX.loop = false;
        FX.Play();
    }

    public void PlayBgm(string audioName, float fadeDuration = 0.1f)
    {
        if (fadeDuration == 0.0f)
        {
            return;
        }
        StopAllCoroutines();
        //CustomAudio sound = Array.Find(backgroundSoundClips, source => source.name == audioName);
        CustomAudio sound = backgroundSoundClips.Find(source => source.name == audioName);

        if (bgm1.isPlaying)
        {
            bgm2.clip = sound.audioClip;
            StartCoroutine(PlaySoundFadeIn(bgm2, fadeDuration));
            StartCoroutine(StopSoundFadeIn(bgm1, fadeDuration));

        }
        else if(bgm2.isPlaying)
        {
            bgm1.clip = sound.audioClip;
            StartCoroutine(PlaySoundFadeIn(bgm1, fadeDuration));
            StartCoroutine(StopSoundFadeIn(bgm2, fadeDuration));
        }
        else
        {
            bgm1.clip = sound.audioClip;
            StartCoroutine(PlaySoundFadeIn(bgm1, fadeDuration));
        }

        // play clip by fade in
        


        // stop clip if there audio is playing.

        
        
    }

    private IEnumerator PlaySoundFadeIn(AudioSource source, float duration)
    {
        //float cofactor = 1.0f / (duration * 100.0f);
        //float cofactor = 1.0f * Time.deltaTime / duration;
        //source.volume = 0.0f;
        source.loop = true;
        source.Play();

        float factor = 0.01f / (duration * 2.0f);

        float tmpVolume = source.volume;
        while(true)
        {
            yield return new WaitForSeconds(factor);
            tmpVolume += factor;
            source.volume = tmpVolume;
            if (source.volume >= 1.0f)
            {
                break;
            }
        }
    }

    private IEnumerator StopSoundFadeIn(AudioSource source, float duration)
    {
        //float cofactor = 1.0f / (duration * 100.0f);
        //float cofactor = 1.0f * Time.deltaTime / duration;
        //source.Play();
        //source.volume = 1.0f;

        float factor = 0.01f / (duration * 2.0f);

        float tmpVolume = source.volume;
        while (true)
        {
            yield return new WaitForSeconds(factor);
            tmpVolume -= factor;
            source.volume = tmpVolume;
            if (source.volume <= 0.0f)
            {
                source.Stop();
                break;
            }
        }
    }

    //public void StopAllSounds()
    //{
    //    foreach(var sound in sounds)
    //    {
    //        sound.source.Stop();
    //    }
    //}


}
