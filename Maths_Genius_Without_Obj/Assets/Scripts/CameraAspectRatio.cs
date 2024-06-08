using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAspectRatio : MonoBehaviour
{
    // Set the default orthographic size for a 16:9 portrait aspect ratio
    public float defaultOrthoSize = 6f;

    private Camera mainCamera;
    private Vector2 referenceAspectRatio = new Vector2(16f, 9f); // Reference aspect ratio (16:9 portrait)

    private void Start()
    {
        mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogError("No main camera found in the scene.");
            return;
        }

        // Calculate the screen's current aspect ratio
        float currentAspectRatio = (float)Screen.width / Screen.height;

        // Calculate the desired orthographic size
        float desiredSize = defaultOrthoSize;

        if (currentAspectRatio != 0)
        {
            // Calculate the aspect ratio difference
            float aspectRatioDiff = referenceAspectRatio.x / referenceAspectRatio.y - currentAspectRatio;

            // Adjust the orthographic size based on the aspect ratio difference
            desiredSize += aspectRatioDiff;
        }

        // Set the camera's orthographic size
        mainCamera.orthographicSize = desiredSize;
    }
}
