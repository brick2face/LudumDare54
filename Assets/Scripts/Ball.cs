using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Ball : MonoBehaviour
{
    public string winScene;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "ElectroGoal")
        {
            SceneManager.LoadScene(winScene);
        }
    }
}
