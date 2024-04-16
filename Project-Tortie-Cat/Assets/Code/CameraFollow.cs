using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // Reference to the player's transform
    public Vector3 offset;   // Offset to adjust the camera's position relative to the player
    public float smoothSpeed = 0.125f;  // Speed at which the camera moves towards the target position and rotation

    private void LateUpdate()
    {
        if (target != null)
        {
            // Calculate the desired camera position based on the player's position and the offset
            Vector3 desiredPosition = target.position - target.forward * offset.magnitude;

            // Smoothly move the camera towards the desired position using SmoothDamp
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Smoothly rotate the camera towards the player's rotation using Slerp
            transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, smoothSpeed);
        }
    }
}