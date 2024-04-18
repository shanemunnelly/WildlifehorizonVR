using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class MonkeyMovement : MonoBehaviour
{
    GameObject player;
    NavMeshAgent agent;

    [SerializeField] LayerMask groundLayer, playerLayer, foodLayer;
    [SerializeField] float range;
    [SerializeField] float sightRange = 10f;
    [SerializeField] float foodDetectionRange = 5f;
    [SerializeField] float animalSpeed = 5f;
    [SerializeField] float carryingFoodSpeed = 2f;
    [SerializeField] float runningSpeed = 8f;
    [SerializeField] float stoppingDistance = 1.5f;
    [SerializeField] private Transform targetEmpty;
    [SerializeField] private float FoodSpeed = 5f;
    [SerializeField] float feedingRange = 3f; // Adjust this value to change feeding range

    bool playerInSight;
    bool foodInSight;
    bool hasFood = false;
    bool isEating = false;
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

        if (Input.GetKeyDown(KeyCode.F) && hasFood && agent.isStopped && !isEating && IsPlayerInRange())
        {
            isEating = true;
            FoodIcon.SetActive(false);
            FoodEatButton.SetActive(false);
            foodItem.SetActive(true);
            animationController.SetBool("isEating", true);
            animationController.SetBool("isRunning", false);

            // Start a coroutine to smoothly move the food item to the target position
            StartCoroutine(MoveFoodItem(targetEmpty.position));

            Invoke("DoneEating", 2f);
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

    private IEnumerator MoveFoodItem(Vector3 targetPosition)
    {
        float distance = Vector3.Distance(foodItem.transform.position, targetPosition);
        while (distance > 0.01f)
        {
            foodItem.transform.position = Vector3.MoveTowards(foodItem.transform.position, targetPosition, FoodSpeed * Time.deltaTime);
            distance = Vector3.Distance(foodItem.transform.position, targetPosition);
            yield return null;
        }
    }

    void DoneEating()
    {
        foodItem.SetActive(false);
        animationController.SetBool("isEating", false);
        animationController.SetBool("isRunning", true);
        agent.isStopped = false;
        isEating = hasFood = false;
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
        agent.speed = runningSpeed;
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
