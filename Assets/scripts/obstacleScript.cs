using UnityEngine;
using System.Collections;

public class obstacleScript : MonoBehaviour {

    public int state;
    public Material tileMat;

    GameObject employeeConstructing;

    float CONSTRUCTIONTIME = 50;

	// Use this for initialization
	void Start () {
        //states:   2 - blocking
        //          1 - being dealt with
        //          0 - removed
        //         -1 - worker on his way
        //         -2 - tiles applied
        state = 2;
	}
	
	// Update is called once per frame
	void Update () {
        if (state == 1)
        {
            if(CONSTRUCTIONTIME <= 0)
            {
                //if it hasn't been constructed yet
                if (gameObject.GetComponent<MeshRenderer>().enabled == false)
                {
                    state = 0;
                    gameObject.GetComponent<NavMeshObstacle>().enabled = false;
                    gameObject.GetComponent<MeshRenderer>().enabled = true;
                    gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
                    gameObject.transform.position = new Vector3(transform.position.x, transform.position.y - .25f, transform.position.z);
                    employeeConstructing.GetComponent<employeeScript>().stopped = false;
                    employeeConstructing.GetComponent<employeeScript>().updateBasedOnState();
                    CONSTRUCTIONTIME = 5;
                }
                //it needs to be tiled now
                else
                {
                    state = -2;
                    GetComponent<MeshRenderer>().material = tileMat;
                    gameObject.GetComponent<Collider>().enabled = false;
                    employeeConstructing.GetComponent<employeeScript>().stopped = false;
                    employeeConstructing.GetComponent<employeeScript>().updateBasedOnState();
                }
            }
            CONSTRUCTIONTIME -= timeManagementScript.globalTimeMultiplier * Time.deltaTime;
        }
	}

    public void updateState(int newState)
    {
        GetComponent<MeshRenderer>().material.color = new Color(.5f,.5f,0,.5f);
    }

    public void abandon()
    {
        state = 2;
    }

    public void setState(int newState)
    {
        state = newState;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "Employee")
        {
            return;
        }
        employeeScript otherScript = other.gameObject.GetComponent<employeeScript>();
        if (otherScript.target.GetInstanceID() == gameObject.GetInstanceID() && otherScript.getState() == 1)
        {
            state = 1;
            otherScript.updateBasedOnState();
            employeeConstructing = other.gameObject;
        }
    }
}
