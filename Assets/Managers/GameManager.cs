using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStoryVariableChangeEvent : UnityEngine.Events.UnityEvent<string, object> { }

public class GameManager : MonoBehaviour
{

    public string InitialSceneName;
    private string m_CurrentSceneName; //Used for saving / loading...

    #region SINGLETON PATTERN 
    private static int m_referenceCount = 0;

    private static GameManager m_instance;

    public static GameManager Instance
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

    #region GAME STORY VARIABLES
    [SerializeField]
    private Dictionary<string, object> m_gameStoryVariables = new Dictionary<string, object>();
    public GameStoryVariableChangeEvent OnGameStoryVariableChanged = new GameStoryVariableChangeEvent();


    /// <summary>
    /// Adds or updates a game story variable to the dictionary.
    /// If the key does not exist, it will be added.
    /// If the key does exist, the value will be updated.
    /// </summary>
    /// <param name="key">The key of the game story variable (e.g. b_HasCompletedPuzzle</param>
    /// <param name="value">The value, returned as an object (can be bool, string, etc.)</param>
    public void SetGameStoryVariable(string key, object value)
    {
        if (m_gameStoryVariables.ContainsKey(key))
        {
            m_gameStoryVariables[key] = value;
        }
        else
        {
            m_gameStoryVariables.Add(key, value);
        }
        Debug.Log("SetGameStoryVariable: " + key + " = " + value.ToString() + " Of type: " + value.GetType().ToString());
        //Fire an event when a game story variable is set, so that other scripts can react to it.
        OnGameStoryVariableChanged.Invoke(key, value);
    }

    /// <summary>
    /// Removes a game story variable from the dictionary.
    /// </summary>
    /// <note>
    /// You probably don't want to use this. Instead, set the value to false or null.
    /// </note>
    /// <param name="key">The key you want to remove.</param>
    public void RemoveGameStoryVariable(string key)
    {
        m_gameStoryVariables.Remove(key);
    }

    /// <summary>
    /// Returns the value of the game story variable with the given key.
    /// </summary>
    /// <typeparam name="T">The type of the object, e.g. bool, string, etc.</typeparam>
    /// <param name="key">The key we are looking up</param>
    /// <returns>The object of a given type.</returns>
    public T GetGameStoryVariable<T>(string key)
    {
        Debug.Log("Checking with key: " + key);
        if (!m_gameStoryVariables.ContainsKey(key))
        {
            return default(T);
        }
        else
        {
            object found = (T)m_gameStoryVariables[key];
            return (T)m_gameStoryVariables[key];
        }
    }
    #endregion

    #region GAME SAVE
    /// <summary>
    /// Saves the game state (via game story variables) to PlayerPrefs.
    /// </summary>
    public void SaveGame()
    {
        // Save all of the game story variables to PlayerPrefs
        foreach (KeyValuePair<string, object> kvp in m_gameStoryVariables)
        {
            PlayerPrefs.SetString(kvp.Key, kvp.Value.ToString());
        }
        // Save which scene we are currently in
        PlayerPrefs.SetString("CurrentScene", m_CurrentSceneName);
    }

    /// <summary>
    /// Loads the game state (via game story variables) from PlayerPrefs.
    /// </summary>
    public void LoadGame()
    {
        foreach (KeyValuePair<string, object> kvp in m_gameStoryVariables)
        {
            if (PlayerPrefs.HasKey(kvp.Key))
            {
                string value = PlayerPrefs.GetString(kvp.Key);
                if (kvp.Value is bool)
                {
                    bool boolValue = bool.Parse(value);
                    m_gameStoryVariables[kvp.Key] = boolValue;
                }
                else if (kvp.Value is string)
                {
                    m_gameStoryVariables[kvp.Key] = value;
                }
            }
        }
        // Load the save game scene
        LoadScene(PlayerPrefs.GetString("CurrentScene"));
    }

    #endregion

    #region GAME AUDIO
    /// <summary>
    /// Plays the given audio clip.
    /// </summary>
    /// <param name="clip">The audio clip to play.</param>
    public void PlaySFX(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }

    /// <summary>
    /// Plays the given audio clip as background music.
    /// </summary>
    /// <param name="clip">The audio clip to play.</param>
    public void PlayBGM(AudioClip clip)
    {
        AudioSource bgmSource = Camera.main.GetComponent<AudioSource>();
        bgmSource.clip = clip;
        bgmSource.Play();
    }
    #endregion

    #region GAME SCENE MANAGEMENT
    /// <summary>
    /// Loads the scene with the given name.
    /// </summary>
    /// <param name="sceneName">SceneName</param>
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        m_CurrentSceneName = sceneName;
        SaveGame();                         // Save the game on a scene change.
    }
    #endregion

    /// <summary>
    /// When we start the game, let's load the initial scene.
    /// </summary>
    void Start()
    {
        LoadScene(InitialSceneName);
    }
}
