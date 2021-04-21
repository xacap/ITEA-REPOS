using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ESoundsFx
{
    //Game
    Booster,
    Collect,
    ColorBomb,
    ShootShort,
    ShootLong,
    //UI
    Button,
    Lose,
    Win
}

public enum EMusicType
{
    MainMenu,
    Game
}
public class CAudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _soundFxSource;
    [SerializeField] private AudioSource _musicSource;

    [SerializeField] private List<SoundFxData> _soundsFXList = new List<SoundFxData>();
    [SerializeField] private List<MusicData> _musicList = new List<MusicData>();

    public static CAudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void PlaySFX(ESoundsFx soundsFx)
    {
        //get neccessary sounds clip
        // set clip and play
        AudioClip clip = GetSoundFXClip(soundsFx);
        _soundFxSource.PlayOneShot(clip);
    }

    public void PlayMusic(EMusicType music)
    {
        _musicSource.clip = GetMusicClip(music);
        _musicSource.Play();
    }

    public void SetSoundFX(bool isOn)
    {
        _soundFxSource.mute = isOn;
    }

    public void SetMusic(bool isOn)
    {
        StartCoroutine(MusicFadeOut(2f));
        //_musicSource.mute = isOn;
    }

    private AudioClip GetSoundFXClip(ESoundsFx eSoundsFx)
    {
        SoundFxData soundData = _soundsFXList.Find(sfxData => sfxData.eSoundFx == eSoundsFx);
        return soundData?.Clip;
    }

    private AudioClip GetMusicClip(EMusicType eMusic)
    {
        MusicData musicData = _musicList.Find(musicfx => musicfx.eMusic == eMusic);
        return musicData?.Clip;
    }

    private IEnumerator MusicFadeOut(float duration = 0.5f)
    {
        var endValue = 0f;
        var startValue = _musicSource.volume;
        var timeCounter = 0f;
        while (timeCounter < duration)
        {
            var normalizedTime = timeCounter / duration;

            _musicSource.volume = Mathf.Lerp(startValue, endValue, normalizedTime);
            timeCounter += Time.deltaTime;
            yield return null;
        }

        _musicSource.volume = 0;
    }
}

[Serializable]
public class SoundFxData
{
    public ESoundsFx eSoundFx;
    public AudioClip Clip;
}


[Serializable]
public class MusicData
{
    public EMusicType eMusic;
    public AudioClip Clip;
}
