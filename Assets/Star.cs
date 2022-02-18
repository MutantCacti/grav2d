using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    Vector3 startingPositionRelativeToCamera;
    Vector3 startingScale;
    public static float dist;
    float zoomScale;
    
    void Start()
    {
        startingPositionRelativeToCamera = transform.position;
        startingScale = transform.localScale;
    }

    void Update()
    {
        zoomScale = Camera.main.orthographicSize / 5;
        transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0) * dist + zoomScale * startingPositionRelativeToCamera;
        transform.localScale = startingScale * zoomScale;
    }
}
