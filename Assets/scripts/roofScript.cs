using UnityEngine;
using System.Collections;

public class roofScript : MonoBehaviour {

    int WIDTH;
    int HEIGHT;

    public GameObject obstacle;
    public GameObject strut;
    public GameObject shingle;
    ArrayList obstacles;
    ArrayList struts;

    // Use this for initialization
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
        struts = new ArrayList();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updateDimensions()
    {
        HEIGHT = GameObject.Find("constructionMaster").GetComponent<constructionMasterScript>().HEIGHT;
        WIDTH = GameObject.Find("constructionMaster").GetComponent<constructionMasterScript>().WIDTH;

        obstacles = new ArrayList();
        for (int i = 0; i < WIDTH; i++)
        {
            for (int j = 0; j < HEIGHT; j++)
            {
                obstacles.Add((GameObject)Instantiate(obstacle, new Vector3(transform.position.x + i - WIDTH / 2,
                    transform.position.y + .5f, transform.position.z + j - HEIGHT / 2), transform.rotation));
            }
        }
    }

    public bool isCompleted()
    {
        for (int i = 0; i < obstacles.Count; i++)
        {
            if (((GameObject)obstacles[i]).GetComponent<MeshRenderer>().enabled == false)
            {
                return false;
            }
        }
        return true;
    }

    public bool isRoofTilesCompleted()
    {
        for (int i = 0; i < obstacles.Count; i++)
        {
            //a row has been completed, so fill in the roof with shingles
            if(i%HEIGHT == 0 && obstacles.Count != 0)
            {
                //TODO
                /*
                GameObject first = (GameObject)Instantiate(shingle, transform.position, transform.rotation);
                first.transform.position = first.transform.position + new Vector3(transform.localScale.x / 2, 0, 0);
                first.transform.localScale = new Vector3(transform.localScale.x / 2, 0, 1);
                first.transform.Rotate(Vector3.right, 45f);
                GameObject second = (GameObject)Instantiate(shingle, transform.position, transform.rotation);
                second.transform.position = second.transform.position + new Vector3(-transform.localScale.x / 2, 0, 0);
                first.transform.localScale = new Vector3(transform.localScale.x / 2, 0, 1);
                second.transform.Rotate(Vector3.right, -45f);
                */
            }
            if (((GameObject)obstacles[i]).GetComponent<roofObstacleScript>().state != -2)
            {
                return false;
            }
        }
        return true;
    }

    public bool anyWorkingSpotsLeft()
    {
        for (int i = 0; i < obstacles.Count; i++)
        {
            if (((GameObject)obstacles[i]).GetComponent<roofObstacleScript>().state == 2)
            {
                return true;
            }
        }
        return false;
    }

    public bool anyWorkingRoofTilesSpotsLeft()
    {
        for (int i = 0; i < obstacles.Count; i++)
        {
            if (((GameObject)obstacles[i]).GetComponent<roofObstacleScript>().state == 0)
            {
                return true;
            }
        }
        return false;
    }

    public void pingForStrutPlacement()
    {
        int completed = 0;
        for(int i = 0; i < obstacles.Count; i++)
        {
            if (((GameObject)obstacles[i]).GetComponent<roofObstacleScript>().state == 0)
                completed++;
        }

        if(struts.Count < Mathf.FloorToInt((float)completed / HEIGHT))
        {
            GameObject newStrut = (GameObject)Instantiate(strut, transform.position + new Vector3((float)completed/HEIGHT - (float)WIDTH/2f - 1f,.5f,0), transform.rotation);
            struts.Add(newStrut);
            newStrut.transform.localScale = new Vector3((float)HEIGHT / 10, (float)HEIGHT / 10, (float)HEIGHT / 10);
            newStrut.transform.position += new Vector3(0, -newStrut.transform.localScale.y / 2, 0);
            if(HEIGHT%2 == 0)
            {
                newStrut.transform.position += new Vector3(0, 0, -.5f);
            }
            if (WIDTH % 2 == 1)
            {
                newStrut.transform.position += new Vector3(.5f, 0, 0);
            }
            //TODO REMOVE THIS WHEN FURTHERING THE CONSTRUCTION PROCESS - THIS IS FOR BETA ONLY
            /*
            if (struts.Count == WIDTH)
            {
                GameObject.Find("employeeAssignmentBlock").GetComponent<employeeAssignmentBlockScript>().showFinalBudget();
            }
            */
        }
    }

    public GameObject getNextRequirement()
    {
        //for returning components for constructing struts
        for (int i = 0; i < obstacles.Count; i++)
        {
            if (((GameObject)obstacles[i]).GetComponent<roofObstacleScript>().state == 2)
            {
                ((GameObject)obstacles[i]).GetComponent<roofObstacleScript>().setState(-1);
                return ((GameObject)obstacles[i]);
            }
        }
        //for finishing the struts
        for (int i = 0; i < obstacles.Count; i++)
        {
            if (((GameObject)obstacles[i]).GetComponent<roofObstacleScript>().state == 0)
            {
                ((GameObject)obstacles[i]).GetComponent<roofObstacleScript>().setState(-1);
                return ((GameObject)obstacles[i]);
            }
        }
        Debug.Log("1948239382");
        return null;
    }
}
