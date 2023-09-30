using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [Header("Display Properties")]
    public bool RequiresVariableToDisplay = false;      // Whether or not this object requires a game story variable to be set to display
    public string ExpectedVariableKey = "";            // The key of the game story variable to check
    public EGameVariableType ExpectedVariableType;     // The type of the game story variable to check
    public string ExpectedVariableValue = null;        // The value of the game story variable to check, this script will handle casting.

    [Header("Game Story On Interact")]
    // Whether or not this object should set a game story variable when interacted with
    public bool ShouldSetGameStoryVariableOnInteract = false;
    public string GameStoryVariableKey = "";            // The key of the game story variable to set
    public EGameVariableType GameStoryVariableType;     // The type of the game story variable to set
    public string GameStoryVariableValue = null;        // The value of the game story variable to set, this script will handle casting.

    [Header("Destruction On Interact")]
    // Whether or not this object should destroy itself when interacted with
    public bool ShouldDestroyOnInteract = false;

    [Header("Inventory On Interact")]
    // Whether or not this object should add an inventory item when interacted with
    public bool ShouldAddInventoryItemOnInteract = false;
    public InventoryItem InventoryItemToAdd = null;      // The inventory item to add
    public int InventoryItemQuantity = 1;               // The quantity of the inventory item to add

    public bool ShouldConsumeInventoryItemOnInteract = false;
    public InventoryItem InventoryItemToConsume = null;  // The inventory item to consume
    public int InventoryItemConsumeQuantity = 1;        // The quantity of the inventory item to consume

    [Header("Scene Load On Interact")]
    // Whether or not this object should load a scene when interacted with
    public bool ShouldLoadSceneOnInteract = false;
    public string SceneName = "";                       // The name of the scene to load

    [Header("SFX on Interact")]
    // Whether or not this object should play a sound effect when interacted with
    public bool ShouldPlaySFXOnInteract = false;
    public AudioClip SFXClip = null;                    // The audio clip to play

    [Header("SFX on Hover")]
    // Whether or not this object should play a sound effect when hovered over
    public bool ShouldPlaySFXOnHover = false;          // Whether or not this object should play a sound effect when hovered over
    public AudioClip HoverSFXClip = null;               // The audio clip to play

    /// <summary>
    /// This is the function that is called when an interactable object is interacted with.
    /// </summary>
    public void Interact()
    {
        // Set the game story variable if we should
        if (ShouldSetGameStoryVariableOnInteract)
        {
            switch (GameStoryVariableType)
            {
                case EGameVariableType.Bool:
                    GameManager.Instance.SetGameStoryVariable(GameStoryVariableKey, bool.Parse(GameStoryVariableValue));
                    break;
                case EGameVariableType.Int:
                    GameManager.Instance.SetGameStoryVariable(GameStoryVariableKey, int.Parse(GameStoryVariableValue));
                    break;
                case EGameVariableType.Float:
                    GameManager.Instance.SetGameStoryVariable(GameStoryVariableKey, float.Parse(GameStoryVariableValue));
                    break;
                case EGameVariableType.String:
                    GameManager.Instance.SetGameStoryVariable(GameStoryVariableKey, GameStoryVariableValue);
                    break;
            }
        }

        // Load a scene if we should
        if (ShouldLoadSceneOnInteract)
        {
            GameManager.Instance.LoadScene(SceneName);
        }

        // Play a sound effect if we should
        if (ShouldPlaySFXOnInteract)
        {
            AudioManager.Instance.PlaySFX(SFXClip);
        }

        // Add an inventory item if we should
        if (ShouldAddInventoryItemOnInteract)
        {
            for (int i = 0; i < InventoryItemQuantity; i++)
            {
                InventoryManager.Instance.GrantInventoryItem(InventoryItemToAdd);
            }
        }

        // Consume an inventory item if we should
        if (ShouldConsumeInventoryItemOnInteract)
        {
            for (int i = 0; i < InventoryItemConsumeQuantity; i++)
            {
                if (InventoryManager.Instance.RemoveInventoryItem(InventoryItemToConsume))
                {
                    //TODO: What should we do if we can't consume the item?
                    //TODO: We should be able to chain Actions together, so that if successful, it may trigger another action.
                    Debug.Log("Consumed an item: " + InventoryItemToConsume.ItemName);

                }
            }
        }

        // Finally, destroy ourselves if we should
        if (ShouldDestroyOnInteract)
        {
            Destroy(gameObject);
        }
    }

    void Awake()
    {
        // We should check whether this object should be displayed or not
        if (RequiresVariableToDisplay)
        {
            // Default the object to not be displayed
            gameObject.SetActive(false);
            CheckDisplay();
            // Performance: We only need to listen for changes if we require a variable to display
            GameManager.Instance.OnGameStoryVariableChanged.AddListener(OnGameStoryVariableChanged);
        }
    }

    void OnGameStoryVariableChanged(string key, object value)
    {
        // Slight performance optimization, if the key doesn't match, we don't need to check
        if (key == ExpectedVariableKey)
        {
            CheckDisplay();
        }
    }

    void CheckDisplay()
    {

        switch (ExpectedVariableType)
        {
            case EGameVariableType.Bool:
                if (GameManager.Instance.GetGameStoryVariable<bool>(ExpectedVariableKey) == bool.Parse(ExpectedVariableValue))
                {
                    gameObject.SetActive(true);
                }
                break;
            case EGameVariableType.Int:
                if (GameManager.Instance.GetGameStoryVariable<int>(ExpectedVariableKey) == int.Parse(ExpectedVariableValue))
                {
                    gameObject.SetActive(true);
                }
                break;
            case EGameVariableType.Float:
                if (GameManager.Instance.GetGameStoryVariable<float>(ExpectedVariableKey) == float.Parse(ExpectedVariableValue))
                {
                    gameObject.SetActive(true);
                }
                break;
            case EGameVariableType.String:
                if (GameManager.Instance.GetGameStoryVariable<string>(ExpectedVariableKey) == ExpectedVariableValue)
                {
                    gameObject.SetActive(true);
                }
                break;
        }
    }

    void OnMouseDown()
    {
        Interact();
    }

    void OnMouseOver()
    {
        // Play a sound effect if we should when hovered over
        if (ShouldPlaySFXOnHover)
        {
            AudioManager.Instance.PlaySFX(HoverSFXClip);
        }
    }


}
