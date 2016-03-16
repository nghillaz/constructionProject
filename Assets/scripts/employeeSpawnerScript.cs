using UnityEngine;
using System.Collections;

public class employeeSpawnerScript : MonoBehaviour {

    public GameObject employee;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {	
	}

    public void spawnEmployee()
    {
        Instantiate(employee, transform.position, transform.rotation);
    }
}
