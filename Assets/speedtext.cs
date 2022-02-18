using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class speedtext : MonoBehaviour
{
    float previousTimeScale;
    Text t;

    void Start() {
        t = GetComponent<Text>();
    }

    void Update()
    {
        if (Time.timeScale != previousTimeScale) {
            if (Time.timeScale == 0f || Time.timeScale == 1f) {
                t.text = ((int) Time.timeScale).ToString() + "x";
            } else if (Time.timeScale > 1f) {
                t.text = ((int) Time.timeScale).ToString() + "x >>";
            } else { 
                t.text = Time.timeScale.ToString() + "x <<";
            } 
        }

        previousTimeScale = Time.timeScale;
    }
}
