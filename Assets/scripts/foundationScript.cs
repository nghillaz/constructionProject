using UnityEngine;
using System.Collections;

public class foundationScript : MonoBehaviour {

    int WIDTH;
    int HEIGHT;

    public GameObject obstacle;
    ArrayList obstacles;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
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
        Debug.Log(obstacles.Count);
    }

    public bool isCompleted()
    {
        for (int i = 0; i < obstacles.Count; i++)
        {
            if (((GameObject)obstacles[i]).GetComponent<MeshRenderer>().enabled != true)
            {
                return false;
            }
        }
        return true;
    }

    public bool isTileCompleted()
    {
        for (int i = 0; i < obstacles.Count; i++)
        {
            if (((GameObject)obstacles[i]).GetComponent<obstacleScript>().state != -2)
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
            if (((GameObject)obstacles[i]).GetComponent<obstacleScript>().state == 2)
            {
                return true;
            }
        }
        return false;
    }

    public bool anyWorkingTileSpotsLeft()
    {
        for (int i = 0; i < obstacles.Count; i++)
        {
            if (((GameObject)obstacles[i]).GetComponent<obstacleScript>().state == 0)
            {
                return true;
            }
        }
        return false;
    }

    public GameObject getNextRequirement()
    {
        //for returning foundation that hasn't been poured yet
        for (int i = 0; i < obstacles.Count; i++)
        {
            if (((GameObject)obstacles[i]).GetComponent<obstacleScript>().state == 2)
            {
                ((GameObject)obstacles[i]).GetComponent<obstacleScript>().setState(-1);
                return ((GameObject)obstacles[i]);
            }
        }
        //for returning any spots that haven't been tiled yet
        for (int i = 0; i < obstacles.Count; i++)
        {
            if (((GameObject)obstacles[i]).GetComponent<obstacleScript>().state == 0)
            {
                ((GameObject)obstacles[i]).GetComponent<obstacleScript>().setState(-1);
                return ((GameObject)obstacles[i]);
            }
        }
        Debug.Log("184823930234");
        return null;
    }
}
