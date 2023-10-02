using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRotation : MonoBehaviour
{
    public float rotationSpeed = 100f; 

    void Update()
    {
        
        float horizontalInput = Input.GetAxis("Horizontal");

        
        transform.Rotate(Vector3.forward * horizontalInput * -rotationSpeed * Time.deltaTime);
    }
}
