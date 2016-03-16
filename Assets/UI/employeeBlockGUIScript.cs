using UnityEngine;
using System.Collections;

public class employeeBlockGUIScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void widthHeightSubmit()
    {
        GameObject.Find("employeeAssignmentBlock").GetComponent<employeeAssignmentBlockScript>().doneWithDimensions();
    }

    public void budgetSubmit()
    {
        GameObject.Find("employeeAssignmentBlock").GetComponent<employeeAssignmentBlockScript>().doneWithBudget();
    }

    public void assignFoundation()
    {
        GameObject.Find("employeeAssignmentBlock").GetComponent<employeeAssignmentBlockScript>().setOrders("foundation");
    }

    public void assignWalls()
    {
        GameObject.Find("employeeAssignmentBlock").GetComponent<employeeAssignmentBlockScript>().setOrders("walls");
    }

    public void assignRoof()
    {
        GameObject.Find("employeeAssignmentBlock").GetComponent<employeeAssignmentBlockScript>().setOrders("roof");
    }

    public void assignTile()
    {
        GameObject.Find("employeeAssignmentBlock").GetComponent<employeeAssignmentBlockScript>().setOrders("tile");
    }

    public void assignDrywall()
    {
        GameObject.Find("employeeAssignmentBlock").GetComponent<employeeAssignmentBlockScript>().setOrders("drywall");
    }

    public void assignRoofTiles()
    {
        GameObject.Find("employeeAssignmentBlock").GetComponent<employeeAssignmentBlockScript>().setOrders("roofTiles");
    }

    public void assignConcreteTruck()
    {
        GameObject.Find("employeeAssignmentBlock").GetComponent<employeeAssignmentBlockScript>().setOrders("concrete");
    }
}