
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clicker : MonoBehaviour
{
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            slider.value += Random.Range(3, 15);
        }else if (Input.GetMouseButtonDown(1))
        {
            slider.value -= Random.Range(3, 15);
        }

        if(slider.value > 0)
        {
            slider.value += 1 * slider.value * Time.deltaTime;
        }else if(slider.value < 0)
        {
            slider.value -= 1 * Mathf.Abs(slider.value) * Time.deltaTime;
        }

        ColorBlock colors = slider.colors;
        colors.normalColor = Color.Lerp(Color.green, Color.red, Mathf.Abs(slider.value) / 50f);
        slider.colors = colors;
    }
}
