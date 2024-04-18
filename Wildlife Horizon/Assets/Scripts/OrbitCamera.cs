using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    public Transform target; // The object to orbit around
    public float distance = 5.0f; // Distance from the object
    public float height = 2.0f; // Height above the object
    public float speed = 1.0f; // Orbit speed

    void Update()
    {
        if (target == null)
            return;

        // Calculate the current rotation angle based on time and speed
        float angle = Time.time * speed;

        // Calculate the position of the camera in a circular orbit around the target
        float x = Mathf.Sin(angle) * distance;
        float z = Mathf.Cos(angle) * distance;

        // Update the camera's position relative to the target
        transform.position = target.position + new Vector3(x, height, z);
        // Make the camera look at the target
        transform.LookAt(target.position);
    }
}
