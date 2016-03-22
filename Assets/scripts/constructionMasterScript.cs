using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class constructionMasterScript : MonoBehaviour {

    public GameObject foundation;
    public GameObject walls;
    public GameObject roof;
    public LayerMask layermask;

    public int productionProgress;

    priorityNode[] finalOutput;

    public int WIDTH;
    public int HEIGHT;
    public float globalBudget;
    public float currentMoney;
    GameObject moneyText;
    
    Camera mainCamera;

    GameObject sendForIdle;

    // Use this for initialization
    void Start () {
        sendForIdle = new GameObject();
        sendForIdle.tag = "IdleObject";

        productionProgress = 0;

        moneyText = GameObject.Find("moneyText");
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        foundation = GameObject.Find("foundation");
        walls = GameObject.Find("walls");
        roof = GameObject.Find("roof");
        
        calculatePriorities();
    }

    public class priorityNode{
        public bool isStart;
        public string name;

        public void initializeName()
        {
            name = name.Substring(0, name.Length - 3);
        }
    }

    public void calculatePriorities()
    {
        //TODO
        //update this when new jobs are added
        float[] priorities = new float[12];
        string[] prioritiesNames = new string[priorities.Length];
        finalOutput = new priorityNode[prioritiesNames.Length];

        priorities[0] = PlayerPrefs.GetFloat("foundationMin");
        priorities[1]  = PlayerPrefs.GetFloat("foundationMax");
        priorities[2]  = PlayerPrefs.GetFloat("wallsMin");
        priorities[3]  = PlayerPrefs.GetFloat("wallsMax");
        priorities[4]  = PlayerPrefs.GetFloat("roofMin");
        priorities[5]  = PlayerPrefs.GetFloat("roofMax");
        priorities[6]  = PlayerPrefs.GetFloat("tileMin");
        priorities[7]  = PlayerPrefs.GetFloat("tileMax");
        priorities[8]  = PlayerPrefs.GetFloat("drywallMin");
        priorities[9]  = PlayerPrefs.GetFloat("drywallMax");
        priorities[10]  = PlayerPrefs.GetFloat("roofTilesMin");
        priorities[11] = PlayerPrefs.GetFloat("roofTilesMax");

        prioritiesNames[0] = "foundationMin";
        prioritiesNames[1] = "foundationMax";
        prioritiesNames[2] = "wallsMin";
        prioritiesNames[3] = "wallsMax";
        prioritiesNames[4] = "roofMin";
        prioritiesNames[5] = "roofMax";
        prioritiesNames[6] = "tileMin";
        prioritiesNames[7] = "tileMax";
        prioritiesNames[8] = "drywallMin";
        prioritiesNames[9] = "drywallMax";
        prioritiesNames[10] = "roofTilesMin";
        prioritiesNames[11] = "roofTilesMax";

        float smallest = 1000f;
        int smallestIndex = -1;
        
        for (int i = 0; i < priorities.Length; i++)
        {
            for(int j = 0; j < priorities.Length; j++)
            {
                if(priorities[j] < smallest && priorities[j] > -1f)
                {
                    smallest = priorities[j];
                    smallestIndex = j;
                }
            }
            priorities[smallestIndex] = -2f;
            finalOutput[i] = new priorityNode();
            finalOutput[i].name = prioritiesNames[smallestIndex];
            finalOutput[i].isStart = prioritiesNames[smallestIndex].Contains("Min");
            finalOutput[i].initializeName();
            smallest = 1000f;
        }
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



    public GameObject getPriorityJob()
    {
        if (productionProgress >= finalOutput.Length)
            return null;

        for (int i = 0; i <= productionProgress; i++)
        {
            if (productionProgress >= finalOutput.Length)
                return null;

            //ok, so we increase the production progress if it's a start ALWAYS
            //we increase the production progress if it's an end && the production of that component is finished && the index == productionProgress
            string orders = finalOutput[i].name;

            //first increase
            if (finalOutput[productionProgress].isStart)
                productionProgress++;

            GameObject potentialJob = getSpecificJob(orders);

            //we have to go idle if we've received idle orders from constructionMaster
            if (potentialJob != null && potentialJob.tag == "IdleObject")
                return null;

            //second increase
            if (!finalOutput[i].isStart && potentialJob == null && i == productionProgress)
                productionProgress++;

            if (potentialJob != null)
            {
                return potentialJob;                
            }
        }
        return null;
    }



    public GameObject getSpecificJob(string orders)
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
                        return walls.GetComponent<wallsScript>().getNextRequirement();
                    }
                }
                break;
            case "roof":
                if (!walls.GetComponent<wallsScript>().isCompleted())
                    return sendForIdle;
                if (walls.GetComponent<wallsScript>().isCompleted() && !roof.GetComponent<roofScript>().isCompleted())
                {
                    //and still some components that can be assigned workers
                    if (roof.GetComponent<roofScript>().anyWorkingSpotsLeft())
                    {
                        return roof.GetComponent<roofScript>().getNextRequirement();
                    }
                }
                break;
            case "tile":
                if (!foundation.GetComponent<foundationScript>().isCompleted())
                    return sendForIdle;
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
                if (!walls.GetComponent<wallsScript>().isCompleted())
                    return sendForIdle;
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
                if (!roof.GetComponent<roofScript>().isCompleted())
                    return sendForIdle;
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
