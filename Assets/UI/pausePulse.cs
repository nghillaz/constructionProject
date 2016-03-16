using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class pausePulse : MonoBehaviour {

    Image main;
    float currentTime;

	// Use this for initialization
	void Start () {
        currentTime = 0;
        main = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        /*
        currentTime += Time.deltaTime;
        currentTime = Mathf.Abs(currentTime);
        float toDisplay = Mathf.Abs(Mathf.Sin(currentTime / 2f))/4f + .7f;
        main.color = new Color(toDisplay, toDisplay, toDisplay);
        */
    }
}
