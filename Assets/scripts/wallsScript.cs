using UnityEngine;
using System.Collections;

public class wallsScript : MonoBehaviour {

    int WIDTH;
    int HEIGHT;

    public GameObject wallPlacementSpot;
    public GameObject ladder;
    ArrayList wallPlacementSpots;
    bool laddersDeployed;

    // Use this for initialization
    void Start()
    {
        laddersDeployed = false; 
    }

    public void updateDimensions()
    {
        HEIGHT = GameObject.Find("constructionMaster").GetComponent<constructionMasterScript>().HEIGHT;
        WIDTH = GameObject.Find("constructionMaster").GetComponent<constructionMasterScript>().WIDTH;

        wallPlacementSpots = new ArrayList();
        for (int i = 0; i < WIDTH; i++)
        {
            for (int j = 0; j < HEIGHT; j++)
            {
                if (i == 0 || j == 0 || i == WIDTH - 1 || j == HEIGHT - 1)
                {
                    GameObject newWall = (GameObject)Instantiate(wallPlacementSpot, new Vector3(transform.position.x + i - WIDTH / 2,
                        transform.position.y + 1f, transform.position.z + j - HEIGHT / 2), transform.rotation);
                    bool isCorner = false;
                    if ((i == 0 && j == 0) || (i == 0 && j == HEIGHT - 1) || (i == WIDTH - 1 && j == HEIGHT - 1) || (i == WIDTH - 1 && j == 0))
                    {
                        Debug.Log("corner");
                        isCorner = true;
                    }
                    int angle = 2;
                    if (i == 0)
                        angle += 0;
                    if (j == 0)
                        angle += 3;
                    if (i == WIDTH - 1)
                        angle += 0;
                    if (j == HEIGHT - 1)
                        angle += 1;
                    if (i == WIDTH - 1 && j == 0)
                        angle -= 1;
                    if (i == 0 && j == HEIGHT - 1)
                        angle -= 1;
                    if (i == WIDTH - 1 && !isCorner)
                        angle += 2;
                    newWall.GetComponent<wallPlacementSpotScript>().setOrientation(angle * 90, isCorner);
                    wallPlacementSpots.Add(newWall);
                }
            }
        }
        Debug.Log(wallPlacementSpots.Count);
    }

        // Update is called once per frame
        void Update()
    {

    }

    public bool isCompleted()
    {
        for (int i = 0; i < wallPlacementSpots.Count; i++)
        {
            if (((GameObject)wallPlacementSpots[i]).GetComponent<MeshRenderer>().enabled != true)
            {
                return false;
            }
        }
        if (!laddersDeployed)
        {
            Debug.Log("ladders deployed");
            deployLadders();
            return true;
        }
        return true;
    }

    public bool isDrywallCompleted()
    {
        for (int i = 0; i < wallPlacementSpots.Count; i++)
        {
            if (((GameObject)wallPlacementSpots[i]).GetComponent<wallPlacementSpotScript>().state != -2)
            {
                return false;
            }
        }
        return true;
    }

    void deployLadders()
    {
        //top
        Instantiate(ladder, transform.position + new Vector3((-(float)WIDTH) / 2f - 2f, 3.5f, 0), transform.rotation);
    }

    public bool anyWorkingSpotsLeft()
    {
        for (int i = 0; i < wallPlacementSpots.Count; i++)
        {
            if (((GameObject)wallPlacementSpots[i]).GetComponent<wallPlacementSpotScript>().state == 2)
            {
                return true;
            }
        }
        return false;
    }

    public bool anyWorkingDrywallSpotsLeft()
    {
        for (int i = 0; i < wallPlacementSpots.Count; i++)
        {
            if (((GameObject)wallPlacementSpots[i]).GetComponent<wallPlacementSpotScript>().state == 0)
            {
                return true;
            }
        }
        return false;
    }

    public GameObject getNextRequirement()
    {
        //for returning walls that are not yet constructed
        for (int i = 0; i < wallPlacementSpots.Count; i++)
        {
            if (((GameObject)wallPlacementSpots[i]).GetComponent<wallPlacementSpotScript>().state == 2)
            {
                ((GameObject)wallPlacementSpots[i]).GetComponent<wallPlacementSpotScript>().setState(-1);
                return ((GameObject)wallPlacementSpots[i]);
            }
        }
        //for returning walls that don't have drywall yet
        for (int i = 0; i < wallPlacementSpots.Count; i++)
        {
            if (((GameObject)wallPlacementSpots[i]).GetComponent<wallPlacementSpotScript>().state == 0)
            {
                ((GameObject)wallPlacementSpots[i]).GetComponent<wallPlacementSpotScript>().setState(-1);
                return ((GameObject)wallPlacementSpots[i]);
            }
        }
        Debug.Log("19283849545");
        return null;
    }
}
