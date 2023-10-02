using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MazeRotation : MonoBehaviour
{
    public float rotationSpeed = 100f; 

    void Update()
    {
        
        float horizontalInput = Input.GetAxis("Horizontal");

        
        transform.Rotate(Vector3.forward * horizontalInput * -rotationSpeed * Time.deltaTime);
    }
    public void reloadScene()
    {
        SceneManager.LoadScene("BallTestScene");
    }
}
