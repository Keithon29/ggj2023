using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{

    float time = 0;
    [SerializeField] float interval = 0.5f;
    public GameObject GrassObject;

    void Grow()
    {
        Instantiate(GrassObject, new Vector3(Random.Range(-29, 30), 0.5f, Random.Range(-19, 20)), Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i=3; i <= 40; i++) {
            Grow();
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= interval) {
            Grow();
            time = 0;
        }
    }
}
