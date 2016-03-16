using UnityEngine;
using System.Collections;

public class wallPlacementSpotScript : MonoBehaviour {
    public Mesh edgeMesh;
    public Mesh cornerMesh;
    public Mesh edgeWIPMesh;
    public Mesh cornerWIPMesh;

    public Material WIPMaterial;
    public Material finalMaterial;

    public int state;
    public string material;

    GameObject employeeConstructing;
    bool isCorner;

    float CONSTRUCTIONTIME = 50;

    // Use this for initialization
    void Start()
    {
        //states:   2 - sitting there - nav Obstacle disabled
        //          1 - being dealt with
        //          0 - finished and blocking (WIP)
        //         -1 - worker is on his way
        //         -2 - finished and blocking (final)
        state = 2;

        GetComponent<MeshRenderer>().enabled = false;
    }

    public void setOrientation(int angle, bool Corner)
    {
        if (Corner)
            GetComponent<MeshFilter>().mesh = cornerWIPMesh;
        else
            GetComponent<MeshFilter>().mesh = edgeWIPMesh;
        GetComponent<MeshRenderer>().material = WIPMaterial;
        transform.rotation *= Quaternion.Euler(new Vector3(-90, angle, 0));
        isCorner = Corner;
    }

    public void setState(int newState)
    {
        state = newState;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == 1)
        {
            if (CONSTRUCTIONTIME <= 0)
            {
                //if you're building the initial framing
                if (GetComponent<MeshRenderer>().enabled == false)
                {
                    GetComponent<MeshRenderer>().enabled = true;
                    GetComponent<MeshRenderer>().material = WIPMaterial;
                    GetComponent<NavMeshObstacle>().enabled = true;
                    transform.position = new Vector3(transform.position.x, transform.position.y + 2.06f, transform.position.z);
                    GetComponent<NavMeshObstacle>().size = new Vector3(.8f, .8f, 5);
                    state = 0;
                    GameObject.Find("walls").GetComponent<wallsScript>().isCompleted();
                    employeeConstructing.GetComponent<employeeScript>().stopped = false;
                    employeeConstructing.GetComponent<employeeScript>().updateBasedOnState();
                    CONSTRUCTIONTIME = 10;
                }
                //if you're putting in the drywall
                else
                {
                    Debug.Log("asdfjkl;");
                    state = -2;
                    if (isCorner)
                        GetComponent<MeshFilter>().mesh = cornerMesh;
                    else
                        GetComponent<MeshFilter>().mesh = edgeMesh;
                    GetComponent<MeshRenderer>().material = finalMaterial;
                    GameObject.Find("walls").GetComponent<wallsScript>().isDrywallCompleted();
                    employeeConstructing.GetComponent<employeeScript>().stopped = false;
                    employeeConstructing.GetComponent<employeeScript>().updateBasedOnState();
                }
            }
            CONSTRUCTIONTIME -= timeManagementScript.globalTimeMultiplier * Time.deltaTime;
        }
    }

    public void abandon()
    {
        state = 2;
    }

    void OnTriggerEnter(Collider other)
    {
        if (state == 0 || state == 1)
        {
            return;
        }
        if (other.gameObject.tag != "Employee")
        {
            return;
        }
        employeeScript otherScript = other.gameObject.GetComponent<employeeScript>();
        if(otherScript.target == null)
        {
            return;
        }
        if (otherScript.target.GetInstanceID() == gameObject.GetInstanceID() && otherScript.getState() == 1)
        {
            state = 1;
            otherScript.updateBasedOnState();
            employeeConstructing = other.gameObject;
        }
    }
}
