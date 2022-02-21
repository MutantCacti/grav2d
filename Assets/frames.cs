using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class frames : MonoBehaviour
{
    Text t;
    float customDeltaTime;
    float lastTimeSinceStartup;

    float waitForNextFrameCounter;
    bool displayFrames;

    void Start() {
        t = GetComponent<Text>();
        t.text = string.Empty;
        displayFrames = false;
    }

    void Update()
    {
        
        
        customDeltaTime = Time.realtimeSinceStartup - lastTimeSinceStartup;

        waitForNextFrameCounter = Mathf.Clamp(waitForNextFrameCounter - customDeltaTime, 0, 0.1f);

        if (Input.GetKeyDown(KeyCode.F)) {
            displayFrames = !displayFrames;
        }

        if (displayFrames) {
            if (waitForNextFrameCounter == 0) {
                t.text = (Mathf.Round((1 / customDeltaTime) * 10.0f) * 0.1f).ToString();
                waitForNextFrameCounter = 0.1f;
            }
        } else {
            t.text = string.Empty;
        }

        lastTimeSinceStartup = Time.realtimeSinceStartup;
    }
}
