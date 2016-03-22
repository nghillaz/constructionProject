using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class onMouseDownScript : MonoBehaviour{

    public GameObject target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        if (target.GetComponent<minSliderScript>())
            target.GetComponent<minSliderScript>().mouse();
        if (target.GetComponent<middleSliderScript>())
            target.GetComponent<middleSliderScript>().mouse();
        if (target.GetComponent<maxSliderScript>())
            target.GetComponent<maxSliderScript>().mouse();
    }
}
