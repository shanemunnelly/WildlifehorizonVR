using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movetowardsisland : MonoBehaviour
{
    public GameObject boat;
    public GameObject land;
    public float speed;
    public float stoppingDistance = 1.0f; // Adjust as needed

    public GameObject player; // Reference to the player GameObject
    private Vector3 playerOffset; // Offset between player and boat positions
    private Vector3 lastBoatPosition; // Last position of the boat

    void Start()
    {
        // Calculate the initial offset between player and boat positions
        playerOffset = player.transform.position - boat.transform.position;
        lastBoatPosition = boat.transform.position;
    }

    void Update()
    {
        // Calculate the direction from the boat to the land
        Vector3 directionToLand = (land.transform.position - boat.transform.position).normalized;

        // Ensure that the direction vector is not pointing directly downwards
        directionToLand.y = Mathf.Max(directionToLand.y, 0.0f);

        // Check if the boat is far enough from the land to move
        float distance = Vector3.Distance(boat.transform.position, land.transform.position);
        if (distance > stoppingDistance)
        {
            // Calculate the displacement of the boat during this frame
            Vector3 displacement = boat.transform.position - lastBoatPosition;

            // Move the boat towards the target position
            boat.transform.position += directionToLand * speed * Time.deltaTime;

            // Apply the same displacement to the player
            player.transform.position += displacement;

            // Update the last boat position
            lastBoatPosition = boat.transform.position;
        }
    }
}
