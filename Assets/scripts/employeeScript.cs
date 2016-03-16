using UnityEngine;
using System.Collections;

public class employeeScript : MonoBehaviour {

    public GameObject target;
    GameObject constructionMaster;
    GameObject concreteMaterial;
    NavMeshAgent agent;
    public int state;
    Animation animation;
    AnimationClip idleAnimation;
    AnimationClip walkAnimation;
    AnimationClip hammerAnimation;
    AnimationClip climbAnimation;
    public float timeMultiplier;

    string currentJob;
    public string truckingMaterial;

    public bool stopped;

    public float wage = 8;
    
	// Use this for initialization
	void Start () {
        //states:   3 - carrying materials to position
        //          2 - fetching materials
        //          1 - applying materials
        //          0 - looking for work
        //         -1 - idle
        //         -2 - climbing ladder
        //         -3 - looking to go up ladder
        //         -4 - looking to go down ladder
        //         -5 - going down ladder
        //         -6 - driving truck
        //         -7 - heading to truck

        Physics.queriesHitTriggers = true;

        constructionMaster = GameObject.Find("constructionMaster");
        concreteMaterial = GameObject.Find("concrete");
        animation = GetComponent<Animation>();
        idleAnimation = animation.GetClip("idle");
        walkAnimation = animation.GetClip("walk");
        hammerAnimation = animation.GetClip("hammer");
        climbAnimation = animation.GetClip("climb");

        stopped = false;

        state = 0;
        agent = GetComponent<NavMeshAgent>();
        agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;

        StartCoroutine("delay");

    }
	
	// Update is called once per frame
	void Update () {
        constructionMaster.GetComponent<constructionMasterScript>().currentMoney -= wage * timeManagementScript.globalTimeMultiplier * Time.deltaTime / 60;
        if(state == -6)
        {
            transform.position = transform.parent.position;
        }

        if (stopped && state != -2 && state != -5)
        {
            GetComponent<NavMeshAgent>().enabled = false;
        }
        else if (state != -2 && state != -5)
        {
            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<NavMeshAgent>().speed = timeManagementScript.globalTimeMultiplier;
        }
        if (state == -2)
        {
            //TODO -- make the climbing scale with fast-forward
            transform.position = new Vector3(transform.position.x, transform.position.y + timeManagementScript.globalTimeMultiplier *Time.deltaTime, transform.position.z);
        }
        else if(state == -5)
        {
            //TODO -- make the climbing scale with fast-forward
            transform.position = new Vector3(transform.position.x, transform.position.y - timeManagementScript.globalTimeMultiplier * Time.deltaTime, transform.position.z);
        }
	}

    public int getState()
    {
        return state;
    }

    public void updateBasedOnState()
    {
        switch (state)
        {
            case 3:
                switch (currentJob)
                {
                    //UPDATE FOR NEW JOBS
                    case "foundation":
                        if (GameObject.Find("foundation").GetComponent<foundationScript>().anyWorkingSpotsLeft())
                        {
                            state = 0;
                            findWork("foundation");
                        }
                        else
                            becomeIdle();
                        break;
                    case "walls":
                        if (GameObject.Find("walls").GetComponent<wallsScript>().anyWorkingSpotsLeft())
                        {
                            state = 0;
                            findWork("walls");
                        }
                        else
                            becomeIdle();
                        break;
                    case "roof":
                        if (GameObject.Find("roof").GetComponent<roofScript>().anyWorkingSpotsLeft())
                        {
                            state = 0;
                            findWork("roof");
                        }
                        else
                            becomeIdle();
                        break;
                    case "tile":
                        if (GameObject.Find("foundation").GetComponent<foundationScript>().anyWorkingTileSpotsLeft())
                        {
                            state = 0;
                            findWork("tile");
                        }
                        else
                            becomeIdle();
                        break;
                    case "drywall":
                        if (GameObject.Find("walls").GetComponent<wallsScript>().anyWorkingDrywallSpotsLeft())
                        {
                            state = 0;
                            findWork("drywall");
                        }
                        else
                            becomeIdle();
                        break;
                    default:
                        becomeIdle();
                        break;
                }
                break;
            case 1:
                constructComponent();
                break;
            case 2:
                goUseMaterials();
                break;
            case -2:
                goUseMaterials();
                break;
            default:
                break;
        }
    }

    public void updateTime(float input)
    {
        animation["idle"].speed = input/4;
        animation["walk"].speed = input/4;
        animation["hammer"].speed = input/4;
        animation["climb"].speed = input/4;
    }

    void OnMouseDown()
    {
    }

    void becomeIdle()
    {
        animation.clip = idleAnimation;
        animation.Play();
        Debug.Log("idling");
        stopped = true;
        GetComponent<NavMeshAgent>().enabled = false;
        state = -1;
    }

    public void abandonJob()
    {
        //abandoning some other task
        if ((state != -1 && state != 0) && target != null)
        {
            Debug.Log("abandoning..." + target.name);
            switch (currentJob)
            {
                //UPDATE FOR NEW JOBS
                case "foundation":
                    target.GetComponent<obstacleScript>().abandon();
                    break;
                case "walls":
                    target.GetComponent<wallPlacementSpotScript>().abandon();
                    break;
                case "roof":
                    target.GetComponent<roofObstacleScript>().abandon();
                    break;
                case "tile":
                    target.GetComponent<obstacleScript>().abandon();
                    break;
                case "drywall":
                    target.GetComponent<wallPlacementSpotScript>().abandon();
                    break;
                case "roofTiles":
                    target.GetComponent<roofObstacleScript>().abandon();
                    break;
                default:
                    break;
            }
        }
    }

    public void truckInMaterials(string material)
    {
        abandonJob();
        if (GameObject.Find("truckManager").GetComponent<truckManagerScript>().isTruckAvailable())
        {
            state = -7;
            stopped = false;
            GetComponent<NavMeshAgent>().enabled = true;
            agent.SetDestination(GameObject.Find("truckManager").GetComponent<truckManagerScript>().getAvailableTruck().transform.position);
            truckingMaterial = material;
            animation.clip = walkAnimation;
            animation.Play();
        }
        else
        {
            becomeIdle();
        }
    }

    public void findWork(string orders)
    {
        abandonJob();

        target = constructionMaster.GetComponent<constructionMasterScript>().getNextRequirement(orders);
        if(target == null)
        {
            becomeIdle();
        }
        else
        {
            currentJob = orders;
            stopped = false;
            GetComponent<NavMeshAgent>().enabled = true;
            fetchMaterial();
        }
    }

    public void fetchMaterial()
    {
        animation.clip = walkAnimation;
        animation.Play();
        GetComponent<NavMeshAgent>().enabled = true;
        if (GetComponent<floorsScript>().floor == 0)
        {
            switch (target.GetComponent<attachMaterial>().material)
            {
                case "concrete":
                    agent.SetDestination(concreteMaterial.transform.position);
                    break;
                default:
                    break;
            }
            state = 2;
        }
        else
        {
            state = -4;
            agent.SetDestination(GameObject.FindGameObjectWithTag("Ladder").transform.position);
        }
    }

    public void constructComponent()
    {
        animation.clip = hammerAnimation;
        animation.Play();
        Debug.Log("constructComponent");
        GetComponent<NavMeshAgent>().enabled = false;
        stopped = true;
        state = 3;
    }

    public void setClimbingUp()
    {
        animation.clip = climbAnimation;
        animation["climb"].speed = Mathf.Abs(animation["climb"].speed);
        animation.Play();
        GameObject ladder = GameObject.FindGameObjectWithTag("Ladder");
        Debug.Log("climbing");
        GetComponent<NavMeshAgent>().enabled = false;
        transform.position = new Vector3(ladder.transform.position.x, transform.position.y, ladder.transform.position.z);
        state = -2;
    }

    public void setClimbingDown()
    {
        animation.clip = climbAnimation;
        animation["climb"].speed = Mathf.Abs(animation["climb"].speed) * -1;
        animation.Play();
        GameObject ladder = GameObject.FindGameObjectWithTag("Ladder");
        Debug.Log("climbing");
        GetComponent<NavMeshAgent>().enabled = false;
        transform.position = new Vector3(ladder.transform.position.x, transform.position.y, ladder.transform.position.z);
        state = -5;
    }

    public void setStopClimbing()
    {
        Debug.Log("stopped climbing");
        if (state == -5)
        {
            state = 2;
            GetComponent<NavMeshAgent>().enabled = true;
            fetchMaterial();
        }
        else
        {
            GetComponent<NavMeshAgent>().enabled = true;
            goUseMaterials();
        }
    }

    public void goUseMaterials()
    {
        GetComponent<NavMeshAgent>().enabled = true;
        if (target.gameObject.GetComponent<floorsScript>().floor == 1 && GetComponent<floorsScript>().floor == 0)
        {
            Debug.Log("ladder is destination");
            agent.SetDestination(GameObject.FindWithTag("Ladder").transform.position);
            state = -3;
        }
        else
        {
            agent.SetDestination(target.transform.position);
            state = 1;
        }
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(.01f);
        updateBasedOnState();
    }
}
