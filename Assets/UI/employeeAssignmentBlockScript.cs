using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class employeeAssignmentBlockScript : MonoBehaviour {

    public GameObject employeeOrders;
    public GameObject blockGradient;
    public GameObject setDimensions;
    public GameObject setBudget;
    public GameObject finalBudget;

    public static GameObject selectedEmployee;

	// Use this for initialization
	void Start () {
        employeeOrders.SetActive(false);
        blockGradient.SetActive(false);
        setDimensions.SetActive(false);
        setBudget.SetActive(false);
        finalBudget.SetActive(false);
        askForDimensions();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void showFinalBudget()
    {
        timeManagementScript.pause();
        blockGradient.SetActive(true);
        finalBudget.SetActive(true);
        GameObject.Find("goal").GetComponent<Text>().text = GameObject.Find("constructionMaster").GetComponent<constructionMasterScript>().globalBudget.ToString();
        GameObject.Find("actual").GetComponent<Text>().text = GameObject.Find("constructionMaster").GetComponent<constructionMasterScript>().currentMoney.ToString();
    }

    public void askForOrders(GameObject employee)
    {
        selectedEmployee = employee;
        employeeOrders.SetActive(true);
        blockGradient.SetActive(true);
        timeManagementScript.pause();
    }

    public void setOrders(string orders)
    {
        if (orders == "concrete" /*or any other truck*/)
        {
            selectedEmployee.GetComponent<employeeScript>().truckInMaterials(orders);
            blockGradient.SetActive(false);
            employeeOrders.SetActive(false);
            timeManagementScript.unPause();
            selectedEmployee = null;
        }
        else
        {
            selectedEmployee.GetComponent<employeeScript>().forceWork(orders);
            blockGradient.SetActive(false);
            employeeOrders.SetActive(false);
            timeManagementScript.unPause();
            selectedEmployee = null;
        }
    }

    void askForDimensions()
    {
        setDimensions.SetActive(true);
        blockGradient.SetActive(true);
        timeManagementScript.pause();
    }

    public void askForBudget()
    {
        setBudget.SetActive(true);
    }

    public void doneWithBudget()
    {
        GameObject.Find("constructionMaster").GetComponent<constructionMasterScript>().initializeBudget(float.Parse(GameObject.Find("budgetNumber").GetComponent<Text>().text));
        setBudget.SetActive(false);
        blockGradient.SetActive(false);
    }

    public void doneWithDimensions()
    {
        GameObject.Find("constructionMaster").GetComponent<constructionMasterScript>().WIDTH = int.Parse(GameObject.Find("widthField").GetComponent<Text>().text);
        GameObject.Find("constructionMaster").GetComponent<constructionMasterScript>().HEIGHT = int.Parse(GameObject.Find("heightField").GetComponent<Text>().text);
        GameObject.Find("constructionMaster").GetComponent<constructionMasterScript>().updateDimensions();
        setDimensions.SetActive(false);
        askForBudget();
    }
}