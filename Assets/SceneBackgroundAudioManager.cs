using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBackgroundAudioManager : MonoBehaviour
{
    public AudioClip BGM;
    public AudioClip BG_Ambience;

    // Start is called before the first frame update
    void Start()
    {
        // Set the background music
        AudioManager.Instance.SetBackgroundMusic(BGM);

        // Set the background ambience
        AudioManager.Instance.SetBackgroundAmbience(BG_Ambience);

    }

}
