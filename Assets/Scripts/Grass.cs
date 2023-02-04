using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{

    float time = 0;
    public GameObject grassObject;

    // Start is called before the first frame update
    void Start()
    {
        for (int i=3; i <= 40; i++) {
            Instantiate(grassObject, new Vector3(Random.Range(-30, 30), 0, Random.Range(-20, 20)), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= 1) {
            Instantiate(grassObject, new Vector3(Random.Range(-30, 30), 0, Random.Range(-20, 20)), Quaternion.identity);
            time = 0;
        }
    }
}
