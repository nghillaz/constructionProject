using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class timeManagementScript : MonoBehaviour {

    public static int globalDays;
    public static int globalHours;
    public static float globalMinutes;
    public static int globalTimeMultiplier;

    static int previousTime;

	// Use this for initialization
	void Start () {
        globalTimeMultiplier = 4;
    }

    public static void pause()
    {
        Debug.Log("pause");
        previousTime = globalTimeMultiplier;
        globalTimeMultiplier = 0;
    }

    public static void unPause()
    {
        Debug.Log("unpause");
        globalTimeMultiplier = previousTime;
    }
	
	// Update is called once per frame
	void Update () {
        globalMinutes += globalTimeMultiplier * Time.deltaTime;

        if (globalMinutes >= 60)
        {
            globalHours++;
            globalMinutes = 0f;
        }
        if(globalHours >= 24)
        {
            globalDays++;
            globalHours = 0;
        }

        string hourText;
        string minuteText;

        if (globalHours > 9)
            hourText = globalHours.ToString();
        else
            hourText = "0" + globalHours.ToString();

        if (Mathf.RoundToInt(globalMinutes) > 9)
            minuteText = (Mathf.RoundToInt(globalMinutes)).ToString();
        else
            minuteText = "0" + (Mathf.RoundToInt(globalMinutes)).ToString();

        GameObject.Find("timeText").GetComponent<Text>().text = "Day: " + globalDays + "      " + hourText + ":" + minuteText;
	}

    public static void goToNextDay()
    {
        globalHours = 7;
        globalMinutes = 0;
        globalDays++;
    }
}
