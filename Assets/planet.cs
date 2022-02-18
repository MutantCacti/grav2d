using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class planet : MonoBehaviour
{
    private const float g = 1;
    
    public GameObject sun;

    public float mass;

    bool velocitylines;
    Rigidbody rb;
    TrailRenderer tr;

    GameObject prevLineX;
    GameObject prevLineY;
    bool wasVelocityLines;

    void Start() {
        velocitylines = cameraScript.velocitylines;

        prevLineX = new GameObject();
        prevLineY = new GameObject();

        tr = GetComponent<TrailRenderer>();
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        if (cameraScript.velocitylines) {

            wasVelocityLines = true;

            Vector3 xline;
            Vector3 yline;
            xline = new Vector3(rb.velocity.x, 0, 0);
            yline = new Vector3(0, rb.velocity.y, 0);

            GameObject myLineX = new GameObject();
            GameObject myLineY = new GameObject();

            myLineX.transform.position = transform.position;
            myLineY.transform.position = transform.position;

            LineRenderer lrx = myLineX.AddComponent<LineRenderer>();
            LineRenderer lry = myLineY.AddComponent<LineRenderer>();

            lrx.material = GetComponent<MeshRenderer>().material;
            lry.material = GetComponent<MeshRenderer>().material;
            lrx.startWidth = 0.04f; lrx.endWidth = 0.01f;
            lry.startWidth = 0.04f; lry.endWidth = 0.01f;

            lrx.SetPosition(0, transform.position);
            lry.SetPosition(0, transform.position);
            lrx.SetPosition(1, transform.position + xline);
            lry.SetPosition(1, transform.position + yline);

            Destroy(prevLineX);
            Destroy(prevLineY);

            prevLineX = myLineX;
            prevLineY = myLineY;
        } else {
            if (wasVelocityLines) {
                Destroy(prevLineX);
                Destroy(prevLineY);
                wasVelocityLines = false;
            }
        }
    }

    void FixedUpdate()
    {   
        var dir = sun.transform.position - transform.position;
        var distance = dir.magnitude;
        dir = dir.normalized;

        rb.AddForce(dir * g * (sun.GetComponent<sun>().mass * mass) / Mathf.Pow(distance, 2));
    }

}
