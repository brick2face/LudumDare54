using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string sceneToLoad; // The name of the scene to load (make sure it's added in Build Settings).

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}