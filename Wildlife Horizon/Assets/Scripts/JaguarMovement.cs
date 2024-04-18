using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class JaguarMovement : MonoBehaviour
{
    GameObject player;
    NavMeshAgent agent;

    [SerializeField] LayerMask groundLayer, playerLayer, foodLayer;
    [SerializeField] float range;
    [SerializeField] float sightRange = 10f;
    [SerializeField] float foodDetectionRange = 5f;
    [SerializeField] float animalSpeed = 5f;
    [SerializeField] float carryingFoodSpeed = 2f;
    [SerializeField] float RunningSpeed = 8f;
    [SerializeField] float stoppingDistance = 1.5f;
    [SerializeField] float feedingRange = 3f; // Adjust this value to change feeding range

    bool playerInSight;
    bool foodInSight;
    bool hasFood = false;
    bool isEating = false;
    bool isFeeding = false; // Added variable to track if the jaguar is feeding
    [SerializeField] GameObject foodItem;
    Animator animationController;
    [SerializeField] GameObject FoodIcon;
    [SerializeField] GameObject FoodEatButton;
    [SerializeField] GameObject book;
    [SerializeField] TextMeshProUGUI mainText;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animationController = GetComponent<Animator>();
        player = GameObject.Find("Player");

        agent.speed = animalSpeed;
    }

    void Update()
    {
        if (isEating) return;

        playerInSight = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        foodInSight = Physics.CheckSphere(transform.position, foodDetectionRange, foodLayer);

        if (hasFood && playerInSight)
        {
            FollowPlayerWithFood();
        }
        else
        {
            if (!playerInSight && !foodInSight) Patrol();
            if (playerInSight) Flee();
        }

        if (Input.GetKeyDown(KeyCode.E)) // Change the key as needed
        {
            InteractWithObject();
        }

        if (Input.GetKeyDown(KeyCode.J)) // Change the key as needed
        {
            book.SetActive(!book.activeSelf);
        }

        // Check if the jaguar is within feeding range and the feeding button is pressed
        if (IsPlayerInRange() && Input.GetKeyDown(KeyCode.F))
        {
            ToggleFeeding();
        }

        // If the jaguar is feeding, perform feeding action
        if (isFeeding && hasFood && agent.isStopped && !isEating)
        {
            FeedAnimal();
        }
        else if (!agent.isStopped)
        {
            FoodEatButton.SetActive(false);
            animationController.SetBool("isRunning", true);
        }
        else
        {
            FoodEatButton.SetActive(true);
            animationController.SetBool("isRunning", false);
        }
    }

    void FeedAnimal()
    {
        isEating = true;
        FoodIcon.SetActive(false);
        FoodEatButton.SetActive(false);
        foodItem.SetActive(true);
        animationController.SetBool("isEating", true);
        animationController.SetBool("isRunning", false);
        Invoke("DoneEating", 2f);
    }

    void ToggleFeeding()
    {
        isFeeding = !isFeeding;
    }

    void DoneEating()
    {
        foodItem.SetActive(false);
        animationController.SetBool("isEating", false);
        animationController.SetBool("isRunning", true);
        agent.isStopped = false;
        isEating = hasFood = isFeeding = false; // Reset feeding flag
        mainText.enabled = true;
    }

    void FollowPlayerWithFood()
    {
        agent.SetDestination(player.transform.position);
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= stoppingDistance)
        {
            agent.velocity = Vector3.zero;
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
            agent.speed = carryingFoodSpeed;
        }
    }

    void Flee()
    {
        Vector3 dirToPlayer = transform.position - player.transform.position;
        Vector3 newPos = transform.position + dirToPlayer;
        agent.speed = RunningSpeed;
        agent.SetDestination(newPos);
    }

    void Patrol()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            SearchForDest();
        }
    }

    void SearchForDest()
    {
        agent.speed = animalSpeed;
        float z = Random.Range(-range, range);
        float x = Random.Range(-range, range);

        Vector3 destPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(destPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    void InteractWithObject()
    {
        Collider interactableCollider = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            interactableCollider = hit.collider;
        }

        if (interactableCollider != null)
        {
            FoodItemGenerator foodGenerator = interactableCollider.GetComponent<FoodItemGenerator>();

            if (foodGenerator != null)
            {
                foodGenerator.GenerateFoodItem();
                hasFood = true;
            }
        }
    }

    bool IsPlayerInRange()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        return distanceToPlayer <= feedingRange;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
