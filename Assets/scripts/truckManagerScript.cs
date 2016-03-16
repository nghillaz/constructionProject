using UnityEngine;
using System.Collections;

public class truckManagerScript : MonoBehaviour {

    public ArrayList trucks;
    public GameObject truck;

	// Use this for initialization
	void Start () {
        trucks = new ArrayList();
        trucks.Add((GameObject)Instantiate(truck, transform.position, transform.rotation));
    }

    public bool isTruckAvailable()
    {
        for(int i = 0; i < trucks.Count; i++)
        {
            if (((GameObject)trucks[i]).GetComponent<truckScript>().state == 0)
                return true;
        }
        return false;
    }

    public GameObject getAvailableTruck()
    {
        for (int i = 0; i < trucks.Count; i++)
        {
            if (((GameObject)trucks[i]).GetComponent<truckScript>().state == 0)
                return (GameObject)trucks[i];
        }
        Debug.Log("18938u23u2i4");
        return null;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
