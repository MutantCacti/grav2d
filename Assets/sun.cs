using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sun : MonoBehaviour
{
    public float mass;
    public float spinSpeed;
    
    Rigidbody rb;
    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + Time.fixedDeltaTime * spinSpeed);
    }

    void OnCollisionEnter(Collision col) {
        Camera.main.gameObject.SendMessage("PlanetDestroyed", col.gameObject);
        GameObject.Destroy(col.gameObject);
        foreach (GameObject obj in Object.FindObjectsOfType<GameObject>()) {
            if (obj.TryGetComponent(out LineRenderer r)) {
                Destroy(obj);
            }
        }
    } 
}
