using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class middleSliderScript : MonoBehaviour {

    float currentValue;
    float deltaValue;

    public bool isClicked;

    public GameObject min;
    public GameObject mid;
    public GameObject max;

	// Use this for initialization
	void Start () {
        isClicked = false;
        if (GetComponent<Slider>())
            currentValue = GetComponent<Slider>().value;
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void mouse()
    {
        min.GetComponent<minSliderScript>().isClicked = false;
        max.GetComponent<maxSliderScript>().isClicked = false;
        isClicked = true;
    }

    public void valueChanged(float value)
    {
        if (!isClicked)
        {
            if (GetComponent<Slider>())
                currentValue = GetComponent<Slider>().value;
            return;
        }
        deltaValue = value - currentValue;

        if (min.GetComponent<Slider>().value + deltaValue <= 0 && deltaValue < 0)
        {
            //min.GetComponent<Slider>().value = 0;
            mid.GetComponent<Slider>().value = currentValue;
            return;
        }
        if (max.GetComponent<Slider>().value + deltaValue >= 1 && deltaValue > 0)
        {
            //max.GetComponent<Slider>().value = 1;
            mid.GetComponent<Slider>().value = currentValue;
            return;
        }

        min.GetComponent<Slider>().value += deltaValue;
        max.GetComponent<Slider>().value += deltaValue;

        currentValue = value;


    }
}
