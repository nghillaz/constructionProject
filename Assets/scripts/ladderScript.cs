using UnityEngine;
using System.Collections;

public class ladderScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Employee")
        {
            return;
        }
        employeeScript otherScript = other.gameObject.GetComponent<employeeScript>();
        if (otherScript.target == null)
        {
            return;
        }
        if (otherScript.getState() == -2)
        {
            other.gameObject.GetComponent<employeeScript>().setStopClimbing();
        }
        else if(otherScript.getState() == -5)
        {
            other.gameObject.GetComponent<employeeScript>().setStopClimbing();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Employee")
        {
            return;
        }
        employeeScript otherScript = other.gameObject.GetComponent<employeeScript>();
        if (otherScript.target == null)
        {
            return;
        }
        if (otherScript.getState() == -3)
        {
            other.gameObject.GetComponent<floorsScript>().floor = 1;
            other.gameObject.GetComponent<employeeScript>().setClimbingUp();
        }
        else if(otherScript.getState() == -4)
        {
            other.gameObject.GetComponent<floorsScript>().floor = 0;
            other.gameObject.GetComponent<employeeScript>().setClimbingDown();
        }
    }
}
