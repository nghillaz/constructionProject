using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class constructionMasterScript : MonoBehaviour {

    public GameObject foundation;
    public GameObject walls;
    public GameObject roof;
    public LayerMask layermask;

    public int WIDTH;
    public int HEIGHT;
    public float globalBudget;
    public float currentMoney;
    GameObject moneyText;

    Camera mainCamera;

    // Use this for initialization
    void Start () {
        moneyText = GameObject.Find("moneyText");
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        foundation = GameObject.Find("foundation");
        walls = GameObject.Find("walls");
        roof = GameObject.Find("roof");
	}

    public void initializeBudget(float input)
    {
        globalBudget = input;
        currentMoney = globalBudget;
    }

    public void togglePauseGame()
    {
    }

    // Update is called once per frame
    void Update () {
        moneyText.GetComponent<Text>().text = currentMoney.ToString();

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, layermask))
            {
                Debug.Log("hit");
                if (hit.collider.gameObject.tag == "Employee")
                {
                    GameObject.Find("employeeAssignmentBlock").GetComponent<employeeAssignmentBlockScript>().askForOrders(hit.collider.gameObject);
                }
            }
        }
    }

    public void updateDimensions()
    {
        foundation.GetComponent<foundationScript>().updateDimensions();
        walls.GetComponent<wallsScript>().updateDimensions();
        roof.GetComponent<roofScript>().updateDimensions();
    }

    

    public void updateTime()
    {
        GameObject[] employees = GameObject.FindGameObjectsWithTag("Employee");
        for(int i = 0; i < employees.Length; i++)
        {
            employees[i].GetComponent<employeeScript>().updateTime(timeManagementScript.globalTimeMultiplier);
        }

        GameObject[] trucks = GameObject.FindGameObjectsWithTag("Truck");
        for (int i = 0; i < trucks.Length; i++)
        {
            trucks[i].GetComponent<truckScript>().updateTime(timeManagementScript.globalTimeMultiplier);
        }
    }

    public GameObject getNextRequirement(string orders)
    {
        switch (orders)
        {
            //UPDATE FOR NEW JOBS
            case "foundation":
                if (!foundation.GetComponent<foundationScript>().isCompleted())
                {
                    //and still some components that can be assigned workers
                    if (foundation.GetComponent<foundationScript>().anyWorkingSpotsLeft())
                    {
                        Debug.Log("foundation component sent");
                        return foundation.GetComponent<foundationScript>().getNextRequirement();
                    }
                }
                break;
            case "walls":
                if (!walls.GetComponent<wallsScript>().isCompleted())
                {
                    //and still some components that can be assigned workers
                    if (walls.GetComponent<wallsScript>().anyWorkingSpotsLeft())
                    {
                        Debug.Log("wall component sent");
                        return walls.GetComponent<wallsScript>().getNextRequirement();
                    }
                }
                break;
            case "roof":
                if (walls.GetComponent<wallsScript>().isCompleted() && !roof.GetComponent<roofScript>().isCompleted())
                {
                    //and still some components that can be assigned workers
                    if (roof.GetComponent<roofScript>().anyWorkingSpotsLeft())
                    {
                        Debug.Log("roof component sent");
                        return roof.GetComponent<roofScript>().getNextRequirement();
                    }
                }
                break;
            case "tile":
                if (foundation.GetComponent<foundationScript>().isCompleted() && !foundation.GetComponent<foundationScript>().isTileCompleted())
                {
                    //and still some components that can be assigned workers
                    if (foundation.GetComponent<foundationScript>().anyWorkingTileSpotsLeft())
                    {
                        Debug.Log("tile component sent");
                        return foundation.GetComponent<foundationScript>().getNextRequirement();
                    }
                }
                break;
            case "drywall":
                if (walls.GetComponent<wallsScript>().isCompleted() && !walls.GetComponent<wallsScript>().isDrywallCompleted())
                {
                    //and still some components that can be assigned workers
                    if (walls.GetComponent<wallsScript>().anyWorkingDrywallSpotsLeft())
                    {
                        Debug.Log("drywall component sent");
                        return walls.GetComponent<wallsScript>().getNextRequirement();
                    }
                    else
                    {
                        Debug.Log("all spots taken");
                    }
                }
                break;
            case "roofTiles":
                if (roof.GetComponent<roofScript>().isCompleted() && !roof.GetComponent<roofScript>().isRoofTilesCompleted())
                {
                    //and still some components that can be assigned workers
                    if (roof.GetComponent<roofScript>().anyWorkingRoofTilesSpotsLeft())
                    {
                        Debug.Log("rooftiles component sent");
                        return roof.GetComponent<roofScript>().getNextRequirement();
                    }
                    else
                    {
                        Debug.Log("all spots taken");
                    }
                }
                break;
            default:
                Debug.Log("never reach here");
                return null;
        }
        return null;
    }
}
