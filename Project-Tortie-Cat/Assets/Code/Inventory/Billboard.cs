using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Declaration of the class "Billboard" which inherits from MonoBehaviour
public class Billboard : MonoBehaviour
{
    // This method is called late in the frame after all Update functions have been called.
    private void LateUpdate()
    {
        // Set the forward direction of the object to match the forward direction of the main camera.
        // This effectively makes the attached object face the camera at all times.
        transform.forward = Camera.main.transform.forward;
    }
}
