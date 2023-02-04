using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private float grassTime = 0; // A variable to track the time since the last grass was placed
    [SerializeField] float GrassInterval = 0.5f; // The interval between each grass placement
    [SerializeField] GameObject GrassObject; // The prefab for the grass

    // Function to place a grass object at a random position
    void PlaceGrass()
    {
        Instantiate(GrassObject, new Vector3(Random.Range(-29, 30), 0.5f, Random.Range(-19, 20)), Quaternion.identity);
    }

    // Place grass objects at the start of the game
    void Start()
    {
        for (int i = 1; i <= 40; i++)
        {
            PlaceGrass();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Increase the grassTime by the deltaTime each frame
        grassTime += Time.deltaTime;
        // If the grassTime is greater than or equal to the GrassInterval, place a grass object and reset grassTime
        if (grassTime >= GrassInterval)
        {
            PlaceGrass();
            grassTime = 0;
        }
    }
}
