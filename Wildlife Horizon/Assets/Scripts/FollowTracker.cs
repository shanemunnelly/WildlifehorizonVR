using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTracker : MonoBehaviour
{
   
    public Transform tracker;
   
    float speed = 2.0f;
   // float accuracy = 1.0f;
    float rotSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 lookAtGoal = new Vector3(tracker.position.x, tracker.position.y, tracker.position.z);
        Vector3 direction = lookAtGoal - this.transform.position;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);

               this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
