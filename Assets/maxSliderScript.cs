using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class maxSliderScript : MonoBehaviour {

    float currentValue;
    float deltaValue;

    public bool isClicked;

    public GameObject min;
    public GameObject mid;
    public GameObject max;

    // Use this for initialization
    void Start()
    {
        isClicked = false;
        if (GetComponent<Slider>())
            currentValue = GetComponent<Slider>().value;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void mouse()
    {
        min.GetComponent<minSliderScript>().isClicked = false;
        mid.GetComponent<middleSliderScript>().isClicked = false;
        isClicked = true;
    }

    public void valueChanged(float value)
    {
        if (GetComponent<Slider>())
            currentValue = GetComponent<Slider>().value;

        if (!isClicked)
            return;

        deltaValue = value - currentValue;
        currentValue = value;

        min.GetComponent<Slider>().value -= deltaValue;
        mid.GetComponent<Slider>().value = (max.GetComponent<Slider>().value + min.GetComponent<Slider>().value) / 2f;
    }
}
