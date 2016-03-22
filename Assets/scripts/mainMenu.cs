using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public GameObject camera;
    public GameObject house;

    public Canvas main;
    public Canvas credit;
    public Canvas instruction;
    public Canvas exit;

    public Button start;

    public Vector3 setting;

    float width = Screen.width / Screen.height;

    void Start()
    {
    }

    public void pressStart()
    {
        Application.LoadLevel("priorityScreen");
    }

    public void pressExit()
    {
        Application.Quit();
    }

    public void pressCredits()
    {
    }

    public void returnFromCredits()
    {
    }

    public void pressInstructions()
    {
        Application.LoadLevel("controls");
    }

    public void returnFromInstructions()
    {
    }
}