using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private float grassTime = 0;
    [SerializeField] float GrassInterval = 0.5f;
    [SerializeField] GameObject GrassObject;
    // Start is called before the first frame update
    void PlaceGrass()
    {
        Instantiate(GrassObject, new Vector3(Random.Range(-29, 30), 0.5f, Random.Range(-19, 20)), Quaternion.identity);
    }
    void Start()
    {
        for (int i=1; i <= 40; i++) {
            PlaceGrass();
        }
    }

    // Update is called once per frame
    void Update()
    {
        grassTime += Time.deltaTime;
        if (grassTime >= GrassInterval) {
            PlaceGrass();
            grassTime = 0;
        }
    }
}
