using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planetmaker : MonoBehaviour
{
    public float growSpeed;
    
    Ray ray;
    RaycastHit hit;
    Material trail;
    Material[] planetColours;

    int clickbuffer = 0;

    void Start() {
        trail = Resources.Load<Material>("trail");
        planetColours = Resources.LoadAll<Material>("Planet Colours");
    }

    void Update()
    {
        if (clickbuffer > 0) clickbuffer--;
        
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit) && Input.GetKey("delete")) {
            Camera.main.SendMessage("PlanetDestroyed", hit.transform.gameObject);
            Destroy(hit.transform.gameObject);
            WipeLines();
        }

        if (Input.GetMouseButtonDown(0)) {
            if (Physics.Raycast(ray, out hit)) {
                StartCoroutine(DrawVelocity(hit));
            } else {
                StartCoroutine(DrawPlanet());
            }
        }
    }

    IEnumerator DrawVelocity(RaycastHit hit) {

        GameObject pastline = new GameObject();
        do {
            
            Destroy(pastline);

            var positionDifference = -1 * (Camera.main.ScreenToWorldPoint(Input.mousePosition) - hit.transform.position); 
            positionDifference.z = 0;
            
            GameObject sun = GameObject.Find("sun");
            GameObject line = new GameObject();
            LineRenderer lr = line.AddComponent<LineRenderer>();
            lr.positionCount = 1000;
            lr.startWidth = 0.07f; lr.endWidth = 0.0f;
            lr.material = Resources.Load<Material>("trail"); //CHANGE MATERIAL LATER

            Vector3 currentPosition = hit.transform.position;
            Vector3 currentVelocity = positionDifference;
            float currentMass = hit.transform.gameObject.GetComponent<planet>().mass;
            for (int i = 0; i < 1000; i++) {
                
                var dir = sun.transform.position - currentPosition;
                var distance = dir.magnitude;
                dir = dir.normalized;

                var force = currentMass * sun.transform.gameObject.GetComponent<sun>().mass / Mathf.Pow(distance, 2);
                currentVelocity += dir * force * Time.fixedDeltaTime;
                currentPosition += currentVelocity * Time.fixedDeltaTime;

                lr.SetPosition(i, currentPosition);
            }

            pastline = line;

            if (Input.GetKey("escape")) {
                yield break;
            }
            yield return null;

        } while (Input.GetMouseButton(0));

        Destroy(pastline);

        Vector3 drawnvelocity = -1 * (Camera.main.ScreenToWorldPoint(Input.mousePosition) - hit.transform.position);
        drawnvelocity.z = 0;
        hit.rigidbody.velocity = drawnvelocity;
    }

    IEnumerator DrawPlanet() {

        GameObject planet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        planet.transform.localScale = new Vector3(0,0,0);

        planet.GetComponent<MeshRenderer>().material = planetColours[Random.Range(0,planetColours.Length)];

        planet.AddComponent<planet>();
        planet.AddComponent<Rigidbody>();
        planet.AddComponent<TrailRenderer>();
        planet.AddComponent<spin>().spinSpeed = Random.Range(1f, 10f);

        planet pscript = planet.GetComponent<planet>();
        TrailRenderer ptr = planet.GetComponent<TrailRenderer>();
        Rigidbody prb = planet.GetComponent<Rigidbody>();

        planet.GetComponent<SphereCollider>().enabled = false;

        pscript.sun = GameObject.Find("sun");
        pscript.mass = 0f;

        prb.useGravity = false;
        prb.interpolation = RigidbodyInterpolation.Interpolate;

        ptr.startWidth = 0.07f; 
        ptr.endWidth = 0.0f;
        ptr.time = 30f;
        ptr.material = Resources.Load<Material>("trail");
        ptr.numCornerVertices = 10;

        float finalmass = 0;
        while (Input.GetMouseButton(0)) {
            if (Input.GetKey("escape")) {
                Destroy(planet);
                yield break;
            }

            planet.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
            planet.transform.localScale += Vector3.one * growSpeed;
            finalmass += growSpeed;
            yield return null;
        }
        planet.GetComponent<SphereCollider>().enabled = true;
        pscript.mass = finalmass;
        gameObject.SendMessage("NewPlanet", planet);
    }

    void WipeLines() {
        foreach (GameObject obj in Object.FindObjectsOfType<GameObject>()) {
            if (obj.TryGetComponent(out LineRenderer r)) {
                Destroy(obj);
            }
        }
    }
}