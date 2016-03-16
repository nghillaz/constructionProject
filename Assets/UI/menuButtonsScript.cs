using UnityEngine;
using System.Collections;

public class menuButtonsScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void playButton()
    {
        Application.LoadLevel("main");
    }

    public void constrolsButton()
    {
        Application.LoadLevel("controls");
    }

    public void instructionsButton()
    {
        Application.LoadLevel("instructions");
    }

    public void menuButton()
    {
        Application.LoadLevel("titlescreen");
    }
}
