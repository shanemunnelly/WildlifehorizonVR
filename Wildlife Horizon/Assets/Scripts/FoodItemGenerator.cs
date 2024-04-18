using UnityEngine;

public class FoodItemGenerator : MonoBehaviour
{
    [SerializeField] GameObject foodItemPrefab;
    [SerializeField] GameObject FoodUI;
    [SerializeField] GameObject interactButton;

    private Camera mainCamera;
    private RaycastHit hit;

    private void Start()
    {
        mainCamera = Camera.main;
        interactButton.SetActive(false); // Initially, hide the interact button
    }

    private void Update()
    {
        // Cast a ray from the main camera
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (Physics.Raycast(ray, out hit))
        {
            // Check if the ray hits the object attached to this script
            if (hit.collider.gameObject == gameObject)
            {
                // Show the interact button
                interactButton.SetActive(true);

                // Check if the player presses the interact button
                if (Input.GetKeyDown(KeyCode.E))
                {
                    GenerateFoodItem();
                }
            }
            else
            {
                // Hide the interact button if the player looks away
                interactButton.SetActive(false);
            }
        }
    }

    public void GenerateFoodItem()
    {
        FoodUI.SetActive(true);

    }
}
