using UnityEngine;
using System.Collections;

// Camera must have trigger collider
// Other objects that prevent the camera from zooming in or out
// should have a rigid body to trigger the camera collider

public class CameraMovement : MonoBehaviour
{

    protected float fDistance = 1;
    protected float fSpeed = 1;
    public GameObject House;
    public GameObject Plane;

    // Variables to decide if the camera is to move or not
    public bool noMove;
    public string lastMove;

    // Key Inputs
    void Update()
    {
        //rotate right
        if (Input.GetKey(KeyCode.RightArrow))
                RotateHouse(true);

        //rotate left
        if (Input.GetKey(KeyCode.LeftArrow))
                RotateHouse(false);

        //zoom in
        if (Input.GetKey(KeyCode.UpArrow))
            MoveInOrOut(false);

        //zoom out
        if (Input.GetKey(KeyCode.DownArrow))
            MoveInOrOut(true);
//        }
    }

    //Rotate House Left Or Right depending on Boolean
    protected void RotateHouse(bool bLeft)
    {
        float step = fSpeed * Time.deltaTime;
        float circumference = 2F * fDistance * Mathf.PI;
        float fDistanceDegrees = (fSpeed / circumference) * 360;
        float fDistanceRadians = (fSpeed / circumference) * 2 * Mathf.PI;

        if (bLeft)
        {
            transform.RotateAround(House.transform.position, Vector3.up, -fDistanceRadians);
        }
        else
            transform.RotateAround(House.transform.position, Vector3.up, fDistanceRadians);
    }

    //Zoom in or out depending on boolean
    protected void MoveInOrOut(bool bOut)
    {
        if (noMove == false)
        {
            if (bOut)
            {
                transform.Translate(0, 0, -fSpeed, Space.Self);
                lastMove = "out";
            }
            else {
                transform.Translate(0, 0, fSpeed, Space.Self);
                lastMove = "in";
            }
        }
        else
        {
            if(lastMove == "in" && bOut)
            {
                transform.Translate(0, 0, -fSpeed, Space.Self);
            }
            else if(lastMove == "out" && !bOut)
            {
                transform.Translate(0, 0, fSpeed, Space.Self);
            }
            //if(lastMove == "in" || !bOut)
            //{
            //    transform.Translate(0, 0, fSpeed, Space.Self);
            //}
        }
    }

    //Sets Boolean noMove to true to prevent camera from zooming into a gameobject
    //or out into space too much
    void OnTriggerEnter(Collider other)
    {
        noMove = true;
        Debug.Log("Collided");
    }

    //Sets Boolean noMove to false to allow the camera to move if doing the opposite
    //of the last action
    void OnTriggerExit(Collider other)
    {
        noMove = false;
        Debug.Log("No Longer");
    }
}