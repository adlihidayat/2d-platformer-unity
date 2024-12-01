using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    public Transform[] backgrounds; // Array of all the back- and foregrounds to be parallaxed
    private float[] parallaxScales; // The proportion of the camera's movement to move the backgrounds by
    public float smoothing = 1f;    // How smooth the parallax effect is (must be greater than 0)

    private Transform cam;          // Reference to the main camera's transform
    private Vector3 previousCamPos; // The position of the camera in the previous frame

    // Ensure references are set before the game starts
    void Awake()
    {
        cam = Camera.main.transform;
    }

    void Start()
    {
        // The previous frame had the current frame's camera position
        previousCamPos = cam.position;

        // Assigning corresponding parallax scales
        parallaxScales = new float[backgrounds.Length];
        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
    }

    void Update()
    {
        // For each background
        for (int i = 0; i < backgrounds.Length; i++)
        {
            // The parallax is the opposite of the camera movement, multiplied by the scale
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

            // Target x position for the background
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            // Create a target position which is the background's current position with its target x position
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            // Smoothly transition between current position and the target position
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        // Set the previousCamPos to the camera's position at the end of the frame
        previousCamPos = cam.position;
    }
}
