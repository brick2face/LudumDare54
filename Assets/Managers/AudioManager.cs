using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public float SFXVolume = 0.7f;
    public float BGMVolume = 0.8f;
    public float AmbientVolume = 0.8f;

    private AudioSource m_bgmSource1;
    private AudioSource m_bgAmbienceSource;
    private AudioSource m_bgmSource2;


    void Start()
    {
        // Create the audio sources
        m_bgmSource1 = gameObject.AddComponent<AudioSource>();
        m_bgmSource2 = gameObject.AddComponent<AudioSource>();
        m_bgAmbienceSource = gameObject.AddComponent<AudioSource>();
        m_bgmSource2.playOnAwake = false;

        // Set the volume
        m_bgmSource1.volume = BGMVolume;
        m_bgmSource2.volume = BGMVolume;
        m_bgAmbienceSource.volume = AmbientVolume;
    }

    #region SINGLETON PATTERN 
    private static int m_referenceCount = 0;

    private static AudioManager m_instance;

    public static AudioManager Instance
    {
        get
        {
            return m_instance;
        }
    }


#pragma warning disable IDE0051

    void Awake()
    {
        m_referenceCount++;
        if (m_referenceCount > 1)
        {
            DestroyImmediate(this.gameObject);
            return;
        }

        m_instance = this;
        // Use this line if you need the object to persist across scenes
        DontDestroyOnLoad(this.gameObject);
    }


    void OnDestroy()
    {
        m_referenceCount--;
        if (m_referenceCount == 0)
        {
            m_instance = null;
        }

    }

#pragma warning restore IDE0051
    #endregion

    #region GAME AUDIO
    /// <summary>
    /// Plays the given audio clip.
    /// </summary>
    /// <param name="clip">The audio clip to play.</param>
    public void PlaySFX(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, SFXVolume);
    }
    #endregion

    #region SCENE AUDIO
    public void SetBackgroundMusic(AudioClip clip)
    {
        m_bgmSource1.clip = clip;
        m_bgmSource2.clip = clip;
        m_bgmSource1.Play();
        StartCoroutine(LoopTracks(clip.length));
    }

    private IEnumerator LoopTracks(float clipLength)
    {
        while (true)
        {
            yield return new WaitForSeconds(clipLength - 10.0f);

            m_bgmSource2.Play();
            yield return new WaitForSeconds(10.0f);

            m_bgmSource2.Stop();

            m_bgmSource1.Play();
            yield return new WaitForSeconds(clipLength - 10.0f);

            m_bgmSource1.Stop();
        }
    }
    public void SetBackgroundAmbience(AudioClip clip)
    {
        m_bgAmbienceSource.clip = clip;
        m_bgAmbienceSource.Play();
    }
    #endregion

}
