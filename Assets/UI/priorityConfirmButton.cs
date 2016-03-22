using UnityEngine;
using System.Collections;

public class priorityConfirmButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void pressed()
    {

        GameObject Foundation = GameObject.Find("foundation");
        PlayerPrefs.SetFloat("foundationMin", Foundation.transform.GetChild(1).GetComponent<middleSliderScript>().min.GetComponent<minSliderScript>().currentValue);
        PlayerPrefs.SetFloat("foundationMax", Foundation.transform.GetChild(1).GetComponent<middleSliderScript>().max.GetComponent<maxSliderScript>().currentValue);

        GameObject Walls = GameObject.Find("walls");
        PlayerPrefs.SetFloat("wallsMin", Walls.transform.GetChild(1).GetComponent<middleSliderScript>().min.GetComponent<minSliderScript>().currentValue);
        PlayerPrefs.SetFloat("wallsMax", Walls.transform.GetChild(1).GetComponent<middleSliderScript>().max.GetComponent<maxSliderScript>().currentValue);

        GameObject Roof = GameObject.Find("roof");
        PlayerPrefs.SetFloat("roofMin", Roof.transform.GetChild(1).GetComponent<middleSliderScript>().min.GetComponent<minSliderScript>().currentValue);
        PlayerPrefs.SetFloat("roofMax", Roof.transform.GetChild(1).GetComponent<middleSliderScript>().max.GetComponent<maxSliderScript>().currentValue);

        GameObject Tile = GameObject.Find("tile");
        PlayerPrefs.SetFloat("tileMin", Tile.transform.GetChild(1).GetComponent<middleSliderScript>().min.GetComponent<minSliderScript>().currentValue);
        PlayerPrefs.SetFloat("tileMax", Tile.transform.GetChild(1).GetComponent<middleSliderScript>().max.GetComponent<maxSliderScript>().currentValue);

        GameObject Drywall = GameObject.Find("drywall");
        PlayerPrefs.SetFloat("drywallMin", Drywall.transform.GetChild(1).GetComponent<middleSliderScript>().min.GetComponent<minSliderScript>().currentValue);
        PlayerPrefs.SetFloat("drywallMax", Drywall.transform.GetChild(1).GetComponent<middleSliderScript>().max.GetComponent<maxSliderScript>().currentValue);

        GameObject RoofTile = GameObject.Find("roofTiles");
        PlayerPrefs.SetFloat("roofTilesMin", RoofTile.transform.GetChild(1).GetComponent<middleSliderScript>().min.GetComponent<minSliderScript>().currentValue);
        PlayerPrefs.SetFloat("roofTilesMax", RoofTile.transform.GetChild(1).GetComponent<middleSliderScript>().max.GetComponent<maxSliderScript>().currentValue);

        Application.LoadLevel("main");
    }

}
