using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// using SubGame;

public class GameManager : MonoBehaviour
{

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

    /// <summary>
    /// Adds a game story variable to the dictionary.
    /// </summary>
    /// <param name="key">The key of the game story variable (e.g. b_HasCompletedPuzzle</param>
    /// <param name="value">The value, returned as an object (can be bool, string, etc.)</param>
    public void AddGameStoryVariable(string key, object value)
    {
        m_gameStoryVariables.Add(key, value);
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
        return (T)m_gameStoryVariables[key];
    }
    #endregion

    #region GAME SAVE
    /// <summary>
    /// Saves the game state (via game story variables) to PlayerPrefs.
    /// </summary>
    public void SaveGame()
    {
        foreach (KeyValuePair<string, object> kvp in m_gameStoryVariables)
        {
            PlayerPrefs.SetString(kvp.Key, kvp.Value.ToString());
        }
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
    }

    #endregion

    /// <summary>
    /// Loads the scene with the given name.
    /// </summary>
    /// <param name="sceneName">SceneName</param>
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    void Start()
    {
        LoadScene("Brig");
    }

}
