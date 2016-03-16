using UnityEngine;
using System.Collections;

public class materialsScript : MonoBehaviour {

    public string material;
    public int amountRemaining;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "Employee")
        {
            return;
        }
        if(other.GetComponent<employeeScript>().getState() == 2)
            other.gameObject.GetComponent<employeeScript>().updateBasedOnState();
    }
}
