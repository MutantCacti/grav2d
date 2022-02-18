using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class spawner : MonoBehaviour
{
    GameObject[] stars = new GameObject[1800];
    
    void Start()
    {
        for (int i = 0; i < stars.Length; i++) {
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            MeshRenderer mr = obj.GetComponent<MeshRenderer>();
            Star str = obj.AddComponent<Star>();
            Destroy(obj.GetComponent<SphereCollider>());

            var scale = Random.Range(0.02f,0.04f);
            obj.transform.localScale = new Vector3(scale, scale, scale);
            mr.material = Resources.Load<Material>("Planet Colours/planet_white");
            obj.transform.position = new Vector3(Random.Range(-18f,18f), Random.Range(-10f,10f), 0);

            Star.dist = Random.Range(0.99f,0.999f);

            stars[i] = obj;
        }
    }
}
