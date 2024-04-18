using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movetowards : MonoBehaviour
{
    public GameObject boat;
    public GameObject land;
    public float speed;
    public float stoppingDistance = 1.0f; // Adjust as needed

    public GameObject player; // Reference to the player GameObject
    private Vector3 playerOffset; // Offset between player and boat positions

    void Start()
    {
        // Calculate the initial offset between player and boat positions
        playerOffset = player.transform.position - boat.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the distance between the boat and the land
        float distance = Vector3.Distance(boat.transform.position, land.transform.position);

        // Check if the boat is far enough from the land to move
        if (distance > stoppingDistance)
        {
            // Calculate the target position for the boat, maintaining its z and y position
            Vector3 targetPosition = new Vector3(land.transform.position.x, boat.transform.position.y, boat.transform.position.z);

            // Move the boat towards the target position
            boat.transform.position = Vector3.MoveTowards(boat.transform.position, targetPosition, speed * Time.deltaTime);

            // Update the player's position relative to the boat
            player.transform.position = boat.transform.position + playerOffset;
        }
    }
}
