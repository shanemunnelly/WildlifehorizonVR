using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FoodItemGenerator : MonoBehaviour
{
    [SerializeField] GameObject foodItemPrefab;
    [SerializeField] GameObject FoodUI;
    [SerializeField] GameObject interactButton;

    private XRGrabInteractable grabInteractable;

    private void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Initially, hide the interact button
        interactButton.SetActive(false);

        // Subscribe to grab events
        grabInteractable.onSelectEntered.AddListener(OnGrab);
        grabInteractable.onSelectExited.AddListener(OnRelease);
    }

    private void OnGrab(XRBaseInteractor interactor)
    {
        GenerateFoodItem();
    }

    private void OnRelease(XRBaseInteractor interactor)
    {
    }
    public void Revealbutton()
    {
        interactButton.SetActive(true);
    }
    public void CloseMenu()
    {
        interactButton.SetActive(false);
    }
    public void GenerateFoodItem()
    {
        FoodUI.SetActive(true);
    }
}
