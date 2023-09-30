using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // How fast the camera moves
    public float CameraMoveSpeed = 1.0f;
    public float LeftExtent = 0.0f;
    public float RightExtent = 0.0f;
    public float UpExtent = 0.0f;
    public float DownExtent = 0.0f;
    public float BufferOffsetX = 3.0f;
    public float BufferOffsetY = 3.0f;

    private float m_CenterOffset;

    void Start()
    {
        // Set the left extent to the left edge of the sprite background which is the parent
        LeftExtent = transform.parent.GetComponent<SpriteRenderer>().bounds.min.x;

        // Set the right extent to the right edge of the sprite background which is the parent
        RightExtent = transform.parent.GetComponent<SpriteRenderer>().bounds.max.x;

        // Set the up extent to the top edge of the sprite background which is the parent
        UpExtent = transform.parent.GetComponent<SpriteRenderer>().bounds.min.y;

        // Set the down extent to the bottom edge of the sprite background which is the parent
        DownExtent = transform.parent.GetComponent<SpriteRenderer>().bounds.max.y;

        // Add a 1 unit buffer to the left and right extents so the camera doesn't go off screen
        LeftExtent += BufferOffsetX;
        RightExtent -= BufferOffsetX;
        UpExtent += BufferOffsetY;
        DownExtent -= BufferOffsetY;
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

        // If mouse is in the top 10% of the screen, move camera up.
        if (Input.mousePosition.y > Screen.height * 0.9f)
        {
            transform.position += Vector3.up * CameraMoveSpeed * Time.deltaTime;

            // If the background is beyond the top edge of the screen, move it back to the top edge of the screen.
            if (transform.position.y > UpExtent)
            {
                transform.position = new Vector3(transform.position.x, UpExtent, transform.position.z);
            }
        }

        // If mouse is in the bottom 10% of the screen, move camera down.
        if (Input.mousePosition.y < Screen.height * 0.1f)
        {
            transform.position += Vector3.down * CameraMoveSpeed * Time.deltaTime;

            // If the background is beyond the bottom edge of the screen, move it back to the bottom edge of the screen.
            if (transform.position.y < DownExtent)
            {
                transform.position = new Vector3(transform.position.x, DownExtent, transform.position.z);
            }
        }
    }
}