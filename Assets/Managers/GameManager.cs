using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStoryVariableChangeEvent : UnityEngine.Events.UnityEvent<string, object> { }

public class GameManager : MonoBehaviour
{

    public string InitialSceneName;
    public string NewGameScene;
    private string m_CurrentSceneName; //Used for saving / loading...
    public Texture2D CursorTexture;

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

        // Saving the inventory is a bit more complicated, because we have a dictionary of InventoryItem -> int
        foreach (KeyValuePair<InventoryItem, int> kvp in InventoryManager.Instance.InventoryItems)
        {
            PlayerPrefs.SetInt(kvp.Key.ItemName, kvp.Value);
        }

        // Save which scene we are currently in
        PlayerPrefs.SetString("CurrentScene", m_CurrentSceneName);
        PlayerPrefs.SetString("HasSavedGame", "true");

        Debug.Log("Saved! Current scene: " + m_CurrentSceneName);
        Debug.Log(PlayerPrefs.GetString("CurrentScene"));
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

        // TODO: Load inventory. I am thinking of custom serialisation (e.g. JSON, but it is open to hacking... on the other hand who cares?)

        // Load the save game scene
        LoadScene(PlayerPrefs.GetString("CurrentScene"));
    }

    /// <summary>
    /// Returns true if there is a saved game.
    /// </summary>
    /// <returns>True if the player has a saved game, or false if not.</returns>
    public bool HasSavedGame()
    {
        return PlayerPrefs.HasKey("HasSavedGame");
    }

    #endregion

    #region GAME SCENE MANAGEMENT

    /// <summary>
    /// Loads the set scene.
    /// </summary>
    public void LoadScene()
    {
        SceneManager.LoadScene(m_CurrentSceneName);
    }

    private bool m_IsFading = false;

    /// <summary>
    /// Loads the scene with the given name.
    /// </summary>
    /// <param name="sceneName">SceneName</param>
    public void LoadScene(string sceneName)
    {
        Debug.Log("received request to load scene: " + sceneName);
        m_CurrentSceneName = sceneName;
        UIManager.Instance.FadeToBlack(2.0f);
        m_IsFading = true;

        // Wait for the fade to finish
        UIManager.Instance.OnFadeFinished.AddListener(() =>
        {
            if (m_IsFading)
            {
                m_IsFading = false;                 // Finished the fade.
                this.LoadScene();                   // Now that we have set the current scene name, we can load the scene.
            }
        });

        // Save the game on a scene change ONLY if we are not going to the main menu or credits.
        if (sceneName != "MainMenu" && sceneName != "Credits")
        {
            SaveGame();
        }
    }
    #endregion

    #region GAME STATE MANAGEMENT
    /// <summary>
    /// Starts a new game.
    /// </summary>
    public void StartNewGame()
    {
        LoadScene(NewGameScene);
    }

    /// <summary>
    /// Loads the saved game.
    /// </summary>
    public void LoadSavedGame()
    {
        if (HasSavedGame())
        {
            Debug.Log("Current saved scene: " + PlayerPrefs.GetString("CurrentScene"));
            LoadScene(PlayerPrefs.GetString("CurrentScene"));
        }
    }

    #endregion

    /// <summary>
    /// When we start the game, let's load the initial scene.
    /// </summary>
    void Start()
    {
        LoadScene(InitialSceneName);
        Cursor.SetCursor(CursorTexture, Vector2.zero, CursorMode.Auto);
    }
}
