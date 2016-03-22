﻿using UnityEngine;
using System.Collections;

public class timeGUIButtonScript : MonoBehaviour {

    constructionMasterScript constructionMaster;

	// Use this for initialization
	void Start () {
        constructionMaster = GameObject.Find("constructionMaster").GetComponent<constructionMasterScript>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void pauseButton()
    {
        timeManagementScript.pause();
        constructionMaster.updateTime();
    }
    public void playButton()
    {
        timeManagementScript.globalTimeMultiplier = 4;
        constructionMaster.updateTime();
    }
    public void fasterButton()
    {
        timeManagementScript.globalTimeMultiplier = 10;
        constructionMaster.updateTime();
    }
    public void fastestButton()
    {
        timeManagementScript.globalTimeMultiplier = 20;
        constructionMaster.updateTime();
    }
    public void addEmployee()
    {
        GameObject.Find("employeeSpawner").GetComponent<employeeSpawnerScript>().spawnEmployee();
    }
    public void reassignEmployees()
    {
        GameObject[] Employees = GameObject.FindGameObjectsWithTag("Employee");
        for(int i = 0; i < Employees.Length; i++)
        {
            if(Employees[i].GetComponent<employeeScript>().state == -1)
                Employees[i].GetComponent<employeeScript>().priorityJob();
        }
    }
}
