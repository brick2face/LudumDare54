using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is used to tell the engine not to destroy the object when a new scene is loaded.
/// </summary>
public class DontDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
