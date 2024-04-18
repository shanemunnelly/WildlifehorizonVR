using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathWay : MonoBehaviour
{

    public UnityStandardAssets.Utility.WaypointCircuit circuit;
    int currentDest = 0;
    float speed = 5.0f;
    float accuracy = 15.0f;
    float rotSpeed = 1.4f;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (circuit.Waypoints.Length == 0)
        {
            return;
        }
        Vector3 lookAtGoal = new Vector3(circuit.Waypoints[currentDest].transform.position.x, circuit.Waypoints[currentDest].transform.position.y, circuit.Waypoints[currentDest].transform.position.z);
        Vector3 direction = lookAtGoal - this.transform.position;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction),Time.deltaTime*rotSpeed);

        if(direction.magnitude < accuracy)
        {
            currentDest++;
            if(currentDest >= circuit.Waypoints.Length)
            {
                currentDest = 0;
            }
        }
        this.transform.Translate(0, 0, speed * Time.deltaTime); 
    }
}
