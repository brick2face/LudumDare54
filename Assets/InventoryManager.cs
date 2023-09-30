using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemAddedEvent : UnityEngine.Events.UnityEvent<InventoryItem, int> { }
public class InventoryItemRemovedEvent : UnityEngine.Events.UnityEvent<InventoryItem, int> { }

public class InventoryManager : MonoBehaviour
{
    #region SINGLETON PATTERN 
    private static int m_referenceCount = 0;

    private static InventoryManager m_instance;

    public static InventoryManager Instance
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

    // The player's inventory
    public Dictionary<InventoryItem, int> InventoryItems = new Dictionary<InventoryItem, int>();

    // Events that fire when an inventory item is added or removed
    public InventoryItemAddedEvent OnInventoryItemAdded = new InventoryItemAddedEvent();
    public InventoryItemRemovedEvent OnInventoryItemRemoved = new InventoryItemRemovedEvent();

    /// <summary>
    /// Grants the player an inventory item
    /// </summary>
    /// <param name="item">The inventory item</param>
    public void GrantInventoryItem(InventoryItem item)
    {
        if (InventoryItems.ContainsKey(item))
        {
            if (InventoryItems[item] < item.MaxStack)
            {
                InventoryItems[item]++;
                OnInventoryItemAdded.Invoke(item, InventoryItems[item]);
            }
            else
            {
                Debug.LogError("Can't add more than the max stack of an item!");
            }
            Debug.Log("Added an item to the player's inventory: " + item.ItemName + " The player now has " + InventoryItems[item] + " of this item.");
        }
        else
        {
            InventoryItems.Add(item, 1);
            OnInventoryItemAdded.Invoke(item, 1);
            Debug.Log("Added an item to the player's inventory: " + item.ItemName + " The player now has " + InventoryItems[item] + " of this item.");
        }

        // Save the game after adding an item
        GameManager.Instance.SaveGame();
    }

    /// <summary>
    /// Removes an inventory item from the player's inventory
    /// </summary>
    /// <param name="item">The item you want to consume.</param>
    /// <returns>Whether the removal was successful.</returns>
    public bool RemoveInventoryItem(InventoryItem item)
    {
        if (InventoryItems.ContainsKey(item))
        {
            int count = InventoryItems[item];
            if (InventoryItems[item] >= 1)
            {
                InventoryItems[item]--;
                if (InventoryItems[item] == 0)
                {
                    InventoryItems.Remove(item);
                }

                // Fire off an event when an item is removed. This can be used for UI updates down the line.
                OnInventoryItemRemoved.Invoke(item, (count - 1));
                Debug.Log("Removed an item from the player's inventory: " + item.ItemName + " The player now has " + (count - 1) + " of this item.");
                // Save the game after removing an item
                GameManager.Instance.SaveGame();
                return true;
            }
        }
        else
        {
            Debug.LogError("Can't remove an item the player doesn't have!");
            return false;
        }
    }
}
