using UnityEngine;
using System.Collections;

public class truckScript : MonoBehaviour {

    public int state;
    public int fetchSize = 100;
    string destinationMaterial;
    float time;

	// Use this for initialization
	void Start () {
        /*states
        0 - available
        1 - on its way out
        2 - on its way in
        */

        state = 0;
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void updateTime(float input)
    {
        time = input;
        GetComponent<NavMeshAgent>().speed = time;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Employee" && state == 0 && collider.gameObject.GetComponent<employeeScript>().state == -7)
        {
            Debug.Log("hey");
            state = 1;
            GetComponent<NavMeshAgent>().destination = GameObject.Find("truckDestination").transform.position;
            GetComponent<NavMeshAgent>().speed = time;
            destinationMaterial = collider.gameObject.GetComponent<employeeScript>().truckingMaterial;
            collider.gameObject.GetComponent<employeeScript>().state = -6;
            collider.gameObject.transform.parent = this.transform;
            collider.gameObject.transform.localPosition = Vector3.zero;
        }

        if (collider.gameObject.tag == "TruckDestination" && state == 1)
        {
            state = 2;
            GetComponent<NavMeshAgent>().destination = GameObject.Find("truckManager").transform.position;
            GetComponent<NavMeshAgent>().speed = time;
        }

        if (collider.gameObject.tag == "TruckStart" && state == 2)
        {
            state = 0;
            GetComponent<NavMeshAgent>().speed = 0;
            //TODO
            //change this so that it goes back to the highest priority work -- requires a redo of "findWork()"
            if (transform.childCount > 0)
            {
                GameObject.Find(transform.GetChild(0).GetComponent<employeeScript>().truckingMaterial).GetComponent<materialsScript>().amountRemaining += fetchSize;
                transform.GetChild(0).GetComponent<employeeScript>().priorityJob();
                transform.DetachChildren();
            }
        }
    } 
}
