using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // How fast the camera moves
    public float CameraMoveSpeed = 1.0f;
    public float LeftExtent = 0.0f;
    public float RightExtent = 0.0f;
    public float BufferOffset = 3.0f;

    private float m_CenterOffset;

    void Start()
    {
        // Set the left extent to the left edge of the sprite background which is the parent
        LeftExtent = transform.parent.GetComponent<SpriteRenderer>().bounds.min.x;

        // Set the right extent to the right edge of the sprite background which is the parent
        RightExtent = transform.parent.GetComponent<SpriteRenderer>().bounds.max.x;

        // Calculate the center offset which is the center of the screen minus one half the width of the screen
        m_CenterOffset = (Screen.width / 2.0f) - LeftExtent;

        // Add a 1 unit buffer to the left and right extents so the camera doesn't go off screen
        LeftExtent += BufferOffset;
        RightExtent -= BufferOffset;

        Debug.Log("LeftExtent: " + LeftExtent + " RightExtent: " + RightExtent);
    }

    // Update is called once per frame
    void Update()
    {
        // If mouse is in the right 10% of the screen, move camera to the right.
        if (Input.mousePosition.x > Screen.width * 0.9f)
        {
            // The camera is a child of the background, so we move the background instead, but not beyond the left edge of the screen.
            transform.position += Vector3.right * CameraMoveSpeed * Time.deltaTime;

            // If the background is beyond the left edge of the screen, move it back to the left edge of the screen.
            if (transform.position.x > RightExtent)
            {
                transform.position = new Vector3(RightExtent, transform.position.y, transform.position.z);
            }
        }

        // If mouse is in the left 10% of the screen, move camera to the left.
        if (Input.mousePosition.x < Screen.width * 0.1f)
        {
            transform.position += Vector3.left * CameraMoveSpeed * Time.deltaTime;

            // If the background is beyond the right edge of the screen, move it back to the right edge of the screen.
            if (transform.position.x < LeftExtent)
            {
                transform.position = new Vector3(LeftExtent, transform.position.y, transform.position.z);
            }
        }
    }
}