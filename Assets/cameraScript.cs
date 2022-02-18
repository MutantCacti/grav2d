using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class cameraScript : MonoBehaviour
{
    public float cameraSpeed;
    public float cameraFastSpeed;

    Vector3 movement;
    float customDeltaTime;
    float lastTimeSinceStartup;
    float previousTimeScale = 1.0f;
    List<GameObject> allPlanets = new List<GameObject>();

    bool focused;
    bool pausetime;
    public static bool velocitylines;
    GameObject focusedPlanet;
    GameObject canvas;
    Vector3 canvasScale;

    void Start() {
        Time.timeScale = 0.0f;
        pausetime = true;
        velocitylines = false;
        focused = false;

        canvas = GameObject.Find("canvas");
        canvasScale = canvas.GetComponent<RectTransform>().localScale;

        allPlanets = GameObject.FindObjectsOfType<GameObject>().ToList();
        foreach (GameObject obj in allPlanets.ToList()) {
            if (!obj.TryGetComponent(out planet planetscript) && !obj.TryGetComponent(out sun sunscript)) {
                allPlanets.Remove(obj);
            }
        }
    }

    Ray ray;
    RaycastHit hit;
    int currentPlanetIndex;

    void Update()
    {
        customDeltaTime = Time.realtimeSinceStartup - lastTimeSinceStartup;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);     

        if (Input.GetKeyDown(KeyCode.Equals)) {
            if (Time.timeScale < 16f) {Time.timeScale *= 2;}
        }

        if (Input.GetKeyDown(KeyCode.Minus)) {
            if (Time.timeScale > 0.0625f) {Time.timeScale /= 2;}
        }

        if (Input.GetMouseButton(1) && Physics.Raycast(ray, out hit)) {
            focusedPlanet = hit.transform.gameObject;
            focused = true;
        }

        movement *= 0.7f;
        if (Input.GetKey("left shift") || Input.GetKey("right shift")) {
            if (Input.GetKey("w")) {movement += Vector3.up * customDeltaTime * cameraFastSpeed; focused = false;}
            if (Input.GetKey("a")) {movement += Vector3.left * customDeltaTime * cameraFastSpeed; focused = false;}
            if (Input.GetKey("s")) {movement += Vector3.down * customDeltaTime * cameraFastSpeed; focused = false;}
            if (Input.GetKey("d")) {movement += Vector3.right * customDeltaTime * cameraFastSpeed; focused = false;}
        } else {
            if (Input.GetKey("w")) {movement += Vector3.up * customDeltaTime * cameraSpeed; focused = false;}
            if (Input.GetKey("a")) {movement += Vector3.left * customDeltaTime * cameraSpeed; focused = false;}
            if (Input.GetKey("s")) {movement += Vector3.down * customDeltaTime * cameraSpeed; focused = false;}
            if (Input.GetKey("d")) {movement += Vector3.right * customDeltaTime * cameraSpeed; focused = false;}
        }
        
        if (focused) {
            if (Input.GetKeyDown(KeyCode.Delete) && focusedPlanet != GameObject.Find("sun")) {
                var temp = focusedPlanet;
                PlanetDestroyed(focusedPlanet);
                Destroy(temp);
                goto SkipToEnd;
            }
            if (Input.GetKey("escape")) {
                focused = false;
            }
            transform.position = new Vector3(focusedPlanet.transform.position.x, focusedPlanet.transform.position.y, -10);
        } else {
            transform.position += movement;
        }
        SkipToEnd:
        if (allPlanets.Count > 0) {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                if (currentPlanetIndex == 0) {
                    currentPlanetIndex = allPlanets.Count - 1;
                } else {currentPlanetIndex--;}
                focusedPlanet = allPlanets[currentPlanetIndex];
                focused = true;
            } 
            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                if (currentPlanetIndex == allPlanets.Count - 1) {
                    currentPlanetIndex = 0;
                } else {currentPlanetIndex++;}
                focusedPlanet = allPlanets[currentPlanetIndex];
                focused = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            Pause();
        }

        if (Input.GetKeyDown(KeyCode.L)) {
            velocitylines = !velocitylines;
        }

        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize + Input.mouseScrollDelta.y * 0.05f, 0.5f, 15f);
        canvas.GetComponent<RectTransform>().localScale = canvasScale * (Camera.main.orthographicSize / 5);

        lastTimeSinceStartup = Time.realtimeSinceStartup;
    }

    void Pause() {
        if (pausetime) {
            Time.timeScale = previousTimeScale;
        } else {
            previousTimeScale = Time.timeScale;
            Time.timeScale = 0.0f;
        }
        pausetime = !pausetime;
    }

    void NewPlanet(GameObject p) {
        allPlanets.Add(p);
    }

    void PlanetDestroyed(GameObject p) {
        allPlanets.Remove(p);
        if (focusedPlanet == p) {
            if (allPlanets.Count > 0) {
                focusedPlanet = allPlanets[0];
            } else {focusedPlanet = null; focused = false;}
        }
        if (currentPlanetIndex > allPlanets.Count - 1) {
            currentPlanetIndex = 0;
        }
    }
}
