using UnityEngine;

public class Ladder : MonoBehaviour
{
    public float climbSpeed = 5f; // Adjust the climb speed in Unity inspector if needed

    private bool isClimbing;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) // Assuming the player has a "Player" tag
        {
            float verticalInput = Input.GetAxis("Vertical");

            if (verticalInput != 0f)
            {
                isClimbing = true;
                Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();
                playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, verticalInput * climbSpeed, playerRigidbody.velocity.z);
            }
            else
            {
                isClimbing = false;
                Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();
                playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, 0f, playerRigidbody.velocity.z); // Stop the player's vertical movement
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isClimbing = false;
        }
    }

    private void Update()
    {
        if (isClimbing)
        {
            // You can add additional climbing animations or adjustments here if needed
        }
    }
}
